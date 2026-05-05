using System;
using System.Data;
using GMDCore;
using GMDCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SPL2;
using SPL2.States.GameStates;
using SPL2.Entities;

namespace SPL2.EnemySpawner;

public class Spawner
{
    private float _spawnInterval = 3f;
    private float _lastSpawnTime = 0f;
    private PlayState _playState;

    private int MaxEnemies = 10;
    private int CurrentEnemies = 0;
    private Random _random = new Random();
    public Spawner(PlayState playState)
    {
        _playState = playState;
    }

    public void Update(GameTime gameTime)
    {
        if (CurrentEnemies >= MaxEnemies) return;

        _lastSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_lastSpawnTime >= _spawnInterval)
        {
            SpawnEnemy();
            _lastSpawnTime = 0f;
        }
    }

    private void SpawnEnemy()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(_playState.Game.Content, "images/atlas-definitions.xml");
        int enemyType = _random.Next(0, 2); // 0 for Melee, 1 for Ranged, add more later
        Sprite enemySprite = atlas.CreateSprite("snake"); // Placeholder, should be different for each type
        BaseEnemy newEnemy = enemyType == 0 
            ? new MeleeEnemy(enemySprite, _playState) 
            : new RangedEnemy(enemySprite, _playState);
        
        float spawnX = Game1.VIRTUAL_WIDTH + 50; // Just off-screen
        float spawnY = _random.Next((int)(Game1.VIRTUAL_HEIGHT * 0.25f), (int)(Game1.VIRTUAL_HEIGHT * 0.75f));
        newEnemy.Position = new Vector2(spawnX, spawnY);
        
        _playState.PendingAdd.Add(newEnemy);
        CurrentEnemies++;
    }

    public void EnemyRemoved()
    {
        CurrentEnemies--;
    }
}