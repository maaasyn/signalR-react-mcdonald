using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace signalR_api.Helpers
{
    public class GroupHelper
    {
        private readonly ClaimsPrincipal _hubCallerContext;

        public GroupHelper(ClaimsPrincipal hubCallerContext)
        {
            _hubCallerContext = hubCallerContext;
        }
        
        public string GroupName(string deviceType)
        {
            var sessionId = _hubCallerContext.FindFirst(c => c.Type == "sessionId").Value;
            var groupName = sessionId + deviceType;
            return groupName;
        }
        
        public string GetGroupName()
        {
            
            var sessionId = _hubCallerContext.FindFirst(c => c.Type == "sessionId").Value;
            var deviceType = _hubCallerContext.FindFirst(c => c.Type == "device").Value;
            var groupName = sessionId + deviceType;
            return groupName;
        }
    }
}