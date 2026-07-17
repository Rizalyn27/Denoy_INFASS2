namespace Denoy_INFASS2.Models
{
    public class Users
    {
        public string _sql (string Username, string Email, string Password, string ConfPass)
        {
            
                return $"From Model: {Username}, {Email}, {Password}, {ConfPass}";

            
        }
    }
}
