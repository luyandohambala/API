using Microsoft.AspNetCore.Mvc.Filters;

namespace SMSAPI.Authentication
{
    public class AuthFilter
    {
        //authentication logic here 
        public AuthFilter()
        {
            
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            //filter apikey logic here 
        }
    }
}
