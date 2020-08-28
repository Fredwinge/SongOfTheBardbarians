CActionPoints and CPower both controll their respective variables for the player

CMetalMode controls metalmode calculations like: which milestone did you reach,
is it on cooldown or not etc.

CMetalModeMilestone contains variables that declare which power treshold needs to
be reached to achieve it and strength of modifiers when reaching it.

CMilestoneManager holds all the milestones within a list, sorts them according
to their power treshold and is used so CMetalMode can reach the milestones easily

CQTESystem controls the QTE, the code is somewhat messy due to being hastely
reworked 2-times to fit time schedules and everchanging design opinions.

CQTESettings should explain itself relativetly well.

CTint was mainly used during development to change base colors of enviroment assets
in runtime to see if they looked better or not