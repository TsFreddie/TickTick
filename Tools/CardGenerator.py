import random
import struct

BOOSTER_RATE = 0.2
SUPER_RATE = 0.1
HARM_RANGE = range(2,5)
SUMMON_RATE = 0.05

cost_rate = [10, 25, 25, 20, 10, 4, 2, 2, 1, 1]

class CardData:
    
    E_FIRE = 0
    E_LIFE = 1
    E_EARTH = 2
    E_WATER = 3
    E_SORCERY = 4
    
    def __init__(self, cid, cost, booster, element):
        self.cid = cid
        self.cost = cost
        self.booster = booster
        self.element = element

class MeleeCardData(CardData):
    def __init__(self, cid, cost, booster, element, power, health, agility):
        super().__init__(cid, cost, booster, element)
        self.power = power
        self.health = health
        self.agility = agility

class RangeCardData(CardData):
    def __init__(self, cid, cost, booster, element, power, health, agility, loss):
        super().__init__(cid, cost, booster, element)
        self.power = power
        self.health = health
        self.agility = agility
        self.loss = loss

class WizardCardData(CardData):
    def __init__(self, cid, cost, booster, element, power):
        super().__init__(cid, cost, booster, element)
        self.power = power

class SummonCardData(CardData):
    def __init__(self, cid, cost, booster, element, power, energy, agility):
        super().__init__(cid, cost, booster, element)
        self.power = power
        self.energy = energy
        self.agility = agility

def randCard(cid, cost):
    card_type = random.randint(0, 2)
    
    is_summon = random.randrange(0, 100)

    
    if (is_summon < 100 * SUMMON_RATE):
        is_summon = True
    else:
        is_summon = False
        
    if (is_summon):
        booster = 1
        element = 1
        power = 1
        energy = 1
        agility = 1
        card = SummonCardData(cid, cost, booster, element, power, energy, agility)
        return card

    if (card_type == 0):
        booster = 1
        element = 1
        power = 1
        health = 1
        agility = 1
        card = MeleeCardData(cid, cost, booster, element, power, health, agility)
        return card

    if (card_type == 1):
        booster = 1
        element = 1
        power = 1
        health = 1
        agility = 1
        loss = 1
        card = RangeCardData(cid, cost, booster, element, power, health, agility, loss)
        return card

    if (card_type == 2):
        booster = 1
        element = 1
        power = 1
        card = WizardCardData(cid, cost, booster, element, power)
        return card
        
def writeCard(card):
    with open("./CardData/" + str(card.cid) + ".txt", "wb") as f:
        if (type(card) is MeleeCardData):
            f.write(struct.pack("Biiiiiii", 0, card.cid, card.cost, card.booster, card.element, card.power, card.health, card.agility))
        if (type(card) is RangeCardData):
            f.write(struct.pack("Biiiiiiii", 1, card.cid, card.cost, card.booster, card.element, card.power, card.health, card.agility, card.loss))
        if (type(card) is WizardCardData):
            f.write(struct.pack("Biiiii", 2, card.cid, card.cost, card.booster, card.element, card.power))
        if (type(card) is SummonCardData):
            f.write(struct.pack("Biiiiiii", 4, card.cid, card.cost, card.booster, card.element, card.power, card.energy, card.agility))

def run():
    i = 0
    cid = 0
    while i < 10:
        writeCard(randCard(cid, i+1))
        cid += 1
        cost_rate[i] -= 1
        if (cost_rate[i] <= 0):
            i += 1

run()
