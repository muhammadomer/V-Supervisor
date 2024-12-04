using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vSuperMTClient.Entities
{
    public enum EnumLastState
    {
        INITIAL = 0,
        RINGINGIN = 1,
        RINGINGOUT = 2,
        CONNECTED = 3,
        HOLD = 4,
        TRANSFER = 5,
        CONFERENCE = 6,
        HANGUP = 7,
        PARK = 8,
        PICKUP = 9,
        ALERT_TRANSFER = 10,
        Hangup_TRANSFER = 11,
        Hangup_Conference = 12,
        CALL_DIVERTED = 13
    }
    public enum EnumInitialStates
    {
        NONE = 0,
        NEW_CALL = 1,
        RECALL = 2,
        TRANSFERCALL = 3,
        CALL_PICKUP = 4,
        TRANSFERIN = 5,
        CALL_DIVERTED = 6
    }
    
}