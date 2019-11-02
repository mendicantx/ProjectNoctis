using Google.Apis.Auth.OAuth2;

namespace GoogleApiWrapper
{
    public class TokenObject
    {
        public UserCredential Credentials { get; set; }
        public string[] TokenScope { get; set; }
        public string Application { get; internal set; }
    }
}