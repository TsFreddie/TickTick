import random
import math

cost_rate = [10, 25, 25, 20, 10, 4, 2, 2, 1, 1]

def getRandParam(cost):
    power = random.randrange(cost,int(2.25 * cost))
    health = random.randrange(1,2*cost)
    agilty = int(100 * math.pow(power,-1) *cost)
    loss = int((power/(0.5 *agilty))* 0.5 * cost)
    booster = int((health/power) * cost *(1/random.randint(1,10)))
    element = random.randint(0,4)
    energy = int(random.randint(1,3) * cost *0.5)
    return (booster,element,agilty,health,power,loss,energy)
