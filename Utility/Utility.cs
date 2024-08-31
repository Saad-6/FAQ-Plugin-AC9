using CommerceBuilder.Users;
using System;

namespace FAQPlugin.Utility
{

    public static class Utility 
    {
        public static string TimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan.TotalDays < 1)
            {
                return "Today";
            }
            else if (timeSpan.TotalDays < 2)
            {
                return "Yesterday";
            }
            else if (timeSpan.TotalDays < 7)
            {
                return $"{timeSpan.Days} days ago";
            }
            else if (timeSpan.TotalDays < 30)
            {
                var weeks = (int)(timeSpan.TotalDays / 7);
                return $"{weeks} {(weeks > 1 ? "weeks" : "week")} ago";
            }
            else if (timeSpan.TotalDays < 365)
            {
                var months = (int)(timeSpan.TotalDays / 30);
                return $"{months} {(months > 1 ? "months" : "month")} ago";
            }
            else
            {
                var years = (int)(timeSpan.TotalDays / 365);
                return $"{years} {(years > 1 ? "years" : "year")} ago";
            }
        }
        public static string GetUserName(User user) 
        {
            if (user.UserName == null)
            {
                return "Anonymous";
            }
            return user.UserName;
        }
    
    }
}
