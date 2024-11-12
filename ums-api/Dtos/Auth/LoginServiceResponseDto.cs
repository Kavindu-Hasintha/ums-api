namespace ums_api.Dtos.Auth
{
    public class LoginServiceResponseDto
    {
        public string NewToken { get; set; }

        // This would be returned to front-end
        public int MyProperty { get; set; }
    }
}
