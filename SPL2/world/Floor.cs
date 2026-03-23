using System;
using System.Data;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace SPL2.World;

// generalized random floor generator for the background, uses some perlin noise
// currently not too clean, and the movement on it is terrible
// need to make the rendering smoother + improving the movement system so that we dont have to regenerate the entire tilemap every time we move a tile, maybe only generate new tiles on the edge and shift the old ones over? also need to make the player movement more natural, currently its just a test input system that moves the player position and then regenerates the tilemap based on that, but ideally we want the player to be able to move around in the center of the screen and then once they reach a certain distance from the center, we lock their position and move the world instead, this way we can have a more natural movement system and not have to worry about the player position being too far from the tilemap center and causing precision issues
// also, make it not skip entire tiles when moving, make each tile float offset so we can move ie. half a tile

// there's a ton of improvements available in this code, in terms of performance, readability, features, generation, etc. just a basic implementation for now
public class Floor
{
    private float _NoiseScale = 0.25f; // guess around with this value till we find something good
    
    // FRACTAL NOISE PARAMS
    private static float FN_FREQUENCY_MULTIPLIER = 2f;
    private static float FN_AMPLITUDE_MULTIPLIER = 0.5f;
    private static int FN_SEED_MULTIPLIER = 1000; // just a random number to offset the seed for each octave, should be large and odd to help with bit mixing
    

    // TILE THRESHOLDS
    // all of these in range 0-1, based on the t value calculated from noise. adjust to change distribution of tiles
    private const float GRASS_THRESHOLD = 0.5f;
    private const float DIRT_THRESHOLD = 0.65f;
    private const float SAND_THRESHOLD = 0.7f;
    private const float PATH_THRESHOLD = 0.8f;


    public Tilemap Tilemap { get; private set; }
    public TextureRegion Background { get; private set; }

    // location
    public double X { get; private set; }
    public double Y { get; private set; }
    private int lastTileX;
    private int lastTileY;

    public long Seed { get; private set; }

    // test stuff
    private const bool DEBUG_PRINTTILE = false;
    private const bool DEBUG_PRINTPOS = false;


    public Floor(Tileset tileset, int TileWidthCount, int TileHeightCount)
    {
        Seed = DateTime.Now.Ticks % 1000000000000; // create a seed based on current time. mod to keep in reasonable range for bitshift later
        Tilemap = new Tilemap(tileset, TileWidthCount, TileHeightCount);
        Generate();
    }

    public void Update(GameTime gameTime) 
    {
        int currentTileX = (int)(X / Tilemap.TileWidth);
        int currentTileY = (int)(Y / Tilemap.TileHeight);

        if (DEBUG_PRINTPOS) Console.WriteLine($"Player position: ({X:F2}, {Y:F2})");

        if (lastTileX == currentTileX && lastTileY == currentTileY)
        {
            return; // still in the same tile, no need to redraw
        }
        lastTileX = currentTileX;
        lastTileY = currentTileY;
        GenerateTilemap(); //regen tilemap after movement (improve this please, this is very inefficient)
        // TODO: performance improvements, only generate new edge tiles and keep rest in memory
    }

    public void Generate()
    {
        GenerateTilemap();
    }

    private void GenerateTilemap()
    {
        int width = Tilemap.Columns;
        int height = Tilemap.Rows;

        // world offset in tiles (X/Y are pixels)
        // do floor here to avoid rounding which sometimes results in generating different tiles for same position
        int baseTileX = (int)Math.Floor(X / Tilemap.TileWidth);
        int baseTileY = (int)Math.Floor(Y / Tilemap.TileHeight);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int world_x = baseTileX + x;
                int world_y = baseTileY + y;

                // smooth field that moves with the world
                float nx = world_x * _NoiseScale;
                float ny = world_y * _NoiseScale;

                float noise = FractalNoise(nx, ny, Seed, 4); // get -1 to 1
                float t = (noise + 1f) * 0.5f;             // 0-1

                int tileIndex;
                if (DEBUG_PRINTTILE) Console.WriteLine($"Tile ({x},{y}): noise={noise:F2}, t={t:F2}");

                // Mostly grass, variants in patches where t is high
                if     (t < GRASS_THRESHOLD) tileIndex = 0; // grass
                else if (t < DIRT_THRESHOLD) tileIndex = 1; // dirt
                else if (t < SAND_THRESHOLD) tileIndex = 2; // sand
                else                         tileIndex = 3; // path

                Tilemap.SetTile(x, y, new Tile(tileIndex));
            }
        }
    }

    public void MoveX(double delta)
    {
        X += delta;
    }

    public void MoveY(double delta)
    {
        Y += delta;
    }

    private static float PerlinNoise2D(float x, float y, long seed)
    {
        int seedOffset = (int)(seed % 0x7FFFFFFF); // Ensure seed is within int range by bitwise operation

        // find unit grid cell containing point
        // again floor is used to avoid rounding issues which can cause generating different tiles for same position
        int x0 = (int)Math.Floor(x);
        int x1 = x0 + 1;
        int y0 = (int)Math.Floor(y);
        int y1 = y0 + 1;

        // relative x and y in grid cell
        float sx = x - x0;
        float sy = y - y0;

        // smooth relative x and y using way too much math
        float n00 = Gradient(seedOffset, x0, y0, x - x0, y - y0);
        float n10 = Gradient(seedOffset, x1, y0, x - x1, y - y0);
        float n01 = Gradient(seedOffset, x0, y1, x - x0, y - y1);
        float n11 = Gradient(seedOffset, x1, y1, x - x1, y - y1);

        // interpolate results
        float ix0 = Lerp(n00, n10, sx);
        float ix1 = Lerp(n01, n11, sx);
        return Lerp(ix0, ix1, sy);
    }

    private static float Lerp(float a, float b, float t) 
    {
        // linear interpolation function, same as math.lerp in unity basically
        return a + t * (b - a);
    }

    private static float Gradient(int seed, int x, int y, float dx, float dy)
    {
        // function to create psuedo random gradient vectors based on grid coords, returns the dot product of the gradient and distance vector

        // random primes for hashing coordinates and seed
        // stolen from https://github.com/COMBINE-lab/rainbowfish/blob/master/xxhash.c
        const int PRIME_X = 374761393;
        const int PRIME_Y = 668265263;
        const int PRIME_SEED = 0x27d4eb2d;

        // bit mix constants
        const int SHIFT_1 = 13;
        const int SHIFT_2 = 16;
        const int MULTIPLIER = 1274126177;

        // insure positive 32 bit integer (31-bit)
        const int MASK = 0x7FFFFFFF;

        // create the hash from grid coordinates and seed
        int hash = (x * PRIME_X + y * PRIME_Y + seed * PRIME_SEED) & MASK;

        // bit mix the hash to get a pseudo-random value
        hash = (hash ^ (hash >> SHIFT_1)) * MULTIPLIER;
        hash = (hash ^ (hash >> SHIFT_2)) * MULTIPLIER;

        // use the hash to determine a gradient direction (4 directions in 2D ++/+-/--/-+)
        float gradX = (hash & 1) == 0 ? 1f : -1f; 
        float gradY = (hash & 2) == 0 ? 1f : -1f; 

        // return dot product of gradient and distance vector
        return (gradX * dx) + (gradY * dy);
    }

    private static float FractalNoise(float x, float y, long seed, int octaves)
    {
        // generate fractal noise by summing multiple octaves of perlin noise
        // each octave has higher frequency and lower amplitude
        float total = 0f;
        float frequency = 1f;
        float amplitude = 1f;
        float maxValue = 0f;

        for (int i = 0; i < octaves; i++)
        {
            total += PerlinNoise2D(x * frequency, y * frequency, seed + i * FN_SEED_MULTIPLIER) * amplitude;

            maxValue += amplitude;
            amplitude *= FN_AMPLITUDE_MULTIPLIER; 
            frequency *= FN_FREQUENCY_MULTIPLIER; 
        }

        return total / maxValue;
    }
}