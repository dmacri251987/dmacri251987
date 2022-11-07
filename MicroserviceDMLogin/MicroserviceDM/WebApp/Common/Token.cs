namespace WebApp.Common
{
    public class Token
    {
        public string TokenString { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }




        public Token GeToken(string token)
        {
            var tokenResult = new Token
            {
                TokenString = token,
                //Se le puede poner el mismo que en la generacion, o si no queres que renueve el token nunca le pones un dia.
                Expires = DateTime.Now.AddHours(4),
                Created = DateTime.Now

            };

            return tokenResult;

        }



        public CookieOptions SetCookiesToken(Token token)
        {

            return new CookieOptions
            {
                HttpOnly = true,
                Expires = token.Expires
            };
        }

    }
}
