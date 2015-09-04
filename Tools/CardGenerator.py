import random
import math
import struct
import csv
import os

BOOSTER_RATE = 0.2
SUPER_RATE = 0.1
HARM_RANGE = range(2,5)
SUMMON_RATE = 0.05
STR_LJUST = 1

csv_path = "./CardData/Card.csv"

cost_rate = [10, 25, 25, 20, 10, 4, 2, 2, 1, 1]
card_list =[]

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
	
    def __str__(self):
	    return str(self.cid).ljust(STR_LJUST)+" Melee ".ljust(STR_LJUST)+"Cost:"+str(self.cost).ljust(STR_LJUST)+" Booster:"+str(self.booster).ljust(STR_LJUST)+" Element:"+str(self.element).ljust(STR_LJUST)+" Power:"+str(self.power).ljust(STR_LJUST)+" Health:"+str(self.health).ljust(STR_LJUST)+" Agility:"+str(self.agility).ljust(STR_LJUST)

class RangeCardData(CardData):
    def __init__(self, cid, cost, booster, element, power, health, agility, loss):
        super().__init__(cid, cost, booster, element)
        self.power = power
        self.health = health
        self.agility = agility
        self.loss = loss
	
    def __str__(self):
	    return str(self.cid).ljust(STR_LJUST)+" Range ".ljust(STR_LJUST)+"Cost:"+str(self.cost).ljust(STR_LJUST)+" Booster:"+str(self.booster).ljust(STR_LJUST)+" Element:"+str(self.element).ljust(STR_LJUST)+" Power:"+str(self.power).ljust(STR_LJUST)+" Health:"+str(self.health).ljust(STR_LJUST)+" Agility:"+str(self.agility).ljust(STR_LJUST)+" Loss:"+str(self.loss).ljust(STR_LJUST)
		
class WizardCardData(CardData):
    def __init__(self, cid, cost, booster, element, power):
        super().__init__(cid, cost, booster, element)
        self.power = power

    def __str__(self):
	    return str(self.cid).ljust(STR_LJUST)+" Wizard ".ljust(STR_LJUST)+"Cost:"+str(self.cost).ljust(STR_LJUST)+" Booster:"+str(self.booster).ljust(STR_LJUST)+" Element:"+str(self.element).ljust(STR_LJUST)+" Power:"+str(self.power).ljust(STR_LJUST)
		
class SummonCardData(CardData):
    def __init__(self, cid, cost, booster, element, power, energy, agility):
        super().__init__(cid, cost, booster, element)
        self.power = power
        self.energy = energy
        self.agility = agility

    def __str__(self):
	    return str(self.cid).ljust(STR_LJUST)+" Summon ".ljust(STR_LJUST)+"Cost:"+str(self.cost).ljust(STR_LJUST)+" Booster:"+str(self.booster).ljust(STR_LJUST)+" Element:"+str(self.element).ljust(STR_LJUST)+" Power:"+str(self.power).ljust(STR_LJUST)+" Energy:"+str(self.energy).ljust(STR_LJUST)+" Agility:"+str(self.agility).ljust(STR_LJUST)
		
def getRandParam2(cost):
    card_type = random.randint(0, 2)
    is_summon = random.randrange(0, 100)
    if (is_summon < 100 * SUMMON_RATE):
        is_summon = True
    else:
        is_summon = False
    health = random.randint(1,random.randint(1,2)*cost) *random.randint(3,7) +random.randint(0,9) *0.01
    power = random.randint(int(health)+1,int(health)*2 +1) -health 
    agility_1 = 1+((100 * math.pow(power,-1) *cost) / random.randint(55,100) + random.randint(1,5))*10
    agility =agility_1
    if(agility_1>=100):
        agility = agility_1 - (int(agility_1/100) * 100)+1
    loss_1 = int((power/(0.5 *(agility/10)))* 0.5 * cost  )
    loss = loss_1
    if(loss_1 >=health):
        if((int(loss_1/health)>= 1)):
            loss = loss_1 - (int(loss_1/health) * health)
        else:
            loss = loss_1 - health + random.randint(1,loss_1 - health)
    booster_1 = random.randint(1,8)*random.randint(0,cost)*0.1 - 0.4*cost +(health/power)*0.05*1.1 +random.randint(1,2)*0.1* health
    booster = booster_1
    if (booster_1 > int(1.5*cost)):
        if((int(booster_1/cost)>= 1.7)):
            booster = 2*booster_1 - ((int(loss_1/cost)-1) * cost) -random.randint(1,cost)+0.01*cost
        else:
            booster = booster_1 - 0.5*cost + random.randint(1,cost)
    element = random.randint(0,4)
    energy = int(random.randint(1,3) * cost *0.5)
    return (card_type,is_summon,int(booster),element,int(agility),int(health),int(power),int(loss),int(energy))

def getRandParam(cost):
    card_type = random.randint(0, 2)
    is_summon = random.randrange(0, 100)
    if (is_summon < 100 * SUMMON_RATE):
        is_summon = True
    else:
        is_summon = False
    health_param = random.randint(1,cost+1)
    health = health_param * 3 +random.randint(0,health_param-1)
    power_param = 1/((12-health_param)/10)
    power = (random.randint(1,cost+2) - random.randint(0,1) )* power_param *1.5
    agility_param = 0.01+ power_param - int(power_param/1)+ random.uniform(0,1-(power_param - int(power_param/1))) 
    agility = 100*agility_param +random.randint(health,2*health)
    booster = (((health_param/10)/power_param)+0.1)*7
    if(agility < 50):
        if(cost>5):
            agility = agility+20
        else:
            agility = agility +30
    i = 0
    while(agility>=100):
        if (random.randint(0,100) >3):
            agility = agility *0.8
            i+=1
        else:
            if(agility == 100):
                agility =100
                break
        if(i>5):
            break;
    loss_param = health/(power+1)
    if (loss_param > 5):
       loss_param = 4
    if (loss_param <1):
       loss_param =1
    loss = (1/loss_param)*health
    element = random.randint(0,4)
    energy = power_param * health*0.3
    return (card_type,is_summon,int(booster),element,int(agility),int(health),int(power),int(loss),int(energy))

def randCard(cid,cost):
    card_type,is_summon,booster,element,agility,health,power,loss,energy = getRandParam(cost)
    if(is_summon):
        return SummonCardData(cid,cost,booster,element,power,energy,agility)
    if(card_type == 0):
        return MeleeCardData(cid,cost,booster,element,power,health,agility)
    if(card_type == 1):
        return RangeCardData(cid,cost,booster,element,power,health,agility,loss)
    if(card_type == 2):
        return WizardCardData(cid, cost, booster, element, power)
        
        
def writeCard(card):
    with open("./CardData/" + str(card.cid) + ".bytes", "wb") as f:
        if (type(card) is MeleeCardData):
            f.write(struct.pack("Biiiiiii", 0, card.cid, card.cost, card.booster, card.element, card.power, card.health, card.agility))
        if (type(card) is RangeCardData):
            f.write(struct.pack("Biiiiiiii", 1, card.cid, card.cost, card.booster, card.element, card.power, card.health, card.agility, card.loss))
        if (type(card) is WizardCardData):
            f.write(struct.pack("Biiiii", 2, card.cid, card.cost, card.booster, card.element, card.power))
        if (type(card) is SummonCardData):
            f.write(struct.pack("Biiiiiii", 4, card.cid, card.cost, card.booster, card.element, card.power, card.energy, card.agility))

def writeCardDataToCsv(card_lists):
    if not(os.path.exists("./CardData/")):
        os.mkdir("./CardData/")
    with open(csv_path,"w",newline="") as f:
        spawmwrite = csv.writer(f,dialect="excel")
        spawmwrite.writerow(["CID", "TYPE", "COST", "BOOSTER", "ELEMENT", "POWER", "HEALTH", "ENERGY", "LOSS", "AGILITY"])
        spawmwrite.writerows(card_list)

def cardDatatoList(card):
    if (type(card) is MeleeCardData):
        card_list.append([card.cid,"Melee",card.cost,card.booster,card.element,card.power,card.health,"","",card.agility])
    if (type(card) is RangeCardData):
        card_list.append([card.cid,"Range",card.cost,card.booster,card.element,card.power,card.health,"",card.loss,card.agility])
    if (type(card) is WizardCardData):
        card_list.append([card.cid,"Wizard",card.cost,card.booster,card.element,card.power,"","","",""])
    if (type(card) is SummonCardData):
        card_list.append([card.cid,"Summon",card.cost,card.booster,card.element,card.power,"",card.energy,"",card.agility])
    
def run():
    i = 0
    cid = 0
    while i < 10:
        writeCard(randCard(cid, i+1))
        cid += 1
        cost_rate[i] -= 1
        if (cost_rate[i] <= 0):
            i += 1
    writeCardDataToCsv(card_list)

def run2():
    i = 0
    cid = 0
    cost_rate_temp = list(cost_rate)
    for n in range(0,100,1):
        card = randCard(cid, i+1)
        cardDatatoList(card)
        print (card)
        cid += 1
        cost_rate_temp[i] -= 1
        
        if(cost_rate_temp[i] <=0):
            i +=1
            if(i == 10):
                cost_rate_temp = list(cost_rate)
                i =0
    writeCardDataToCsv(card_list)
    
run()
