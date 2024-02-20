import time
import random

class DifficultyAdjuster:

    minBalloonPerSpawn = 1
    maxBalloonPerSpawn = 5

    minSpeed = 1.0
    maxSpeed = 5.0

    minSpawnTime  = 1.5
    maxSpawnTime = 4.5

    def __init__(self):
        self.last_time = time.time()
        self.game_start_time = time.time()


    #first 8 seconds only spawn 1 balloon then increase the probability of spawn rate 
    def getBalloonSpawn(self):
        current_time = time.time()
        if current_time - self.game_start_time < 8:
            return DifficultyAdjuster.minBalloonPerSpawn
        elif current_time - self.last_time >= 3:
            DifficultyAdjuster.minBalloonPerSpawn = min(DifficultyAdjuster.minBalloonPerSpawn + 1, DifficultyAdjuster.maxBalloonPerSpawn)
            self.last_time = current_time

        if(DifficultyAdjuster.minBalloonPerSpawn == DifficultyAdjuster.maxBalloonPerSpawn): return DifficultyAdjuster.maxBalloonPerSpawn
        return random.randrange(DifficultyAdjuster.minBalloonPerSpawn, DifficultyAdjuster.maxBalloonPerSpawn)



    #increase the probability of getting high speed balloon in every 3 seconds
    def balloonSpeed(self):                                
        current_time = time.time()
        if current_time - self.last_time >= 3:
            DifficultyAdjuster.minSpeed = min(DifficultyAdjuster.minSpeed + 0.5, DifficultyAdjuster.maxSpeed)
            DifficultyAdjuster.maxSpeed = min(DifficultyAdjuster.maxSpeed + 1, 10)  # Assuming 10 as the maximum speed limit
            self.last_time = current_time
        
        if(DifficultyAdjuster.minSpeed == DifficultyAdjuster.maxSpeed): return DifficultyAdjuster.maxSpeed
        return random.uniform(DifficultyAdjuster.minSpeed , DifficultyAdjuster.maxSpeed)
    

    # Decrease spawn time every 3 seconds
    def spawnRate(self):
     elapsed_time = time.time() - self.game_start_time
     if elapsed_time < 3:
        return DifficultyAdjuster.maxSpawnTime
     else:
        spawnTime = max(DifficultyAdjuster.maxSpawnTime - (elapsed_time / 3), DifficultyAdjuster.minSpawnTime)  
        return spawnTime


