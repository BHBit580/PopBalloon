import random

class DifficultyAdjuster:

    def generate_values(self):
        return random.randrange(1, 10), random.randrange(0, 20)

    def randomBalloonSpawn(self):
        return random.randint(1 , 4) 
        
    def randomBalloonSpeed(self):
        return random.uniform(0.5 , 3)
