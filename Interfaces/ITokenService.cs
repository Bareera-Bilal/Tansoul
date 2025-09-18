
namespace P1WEBMVC.Interfaces;

public interface ITokenService
{
public string CreateToken(Guid userId, string email, string username,  int time);

public string CreateToken(Guid userId, string email, string username, Types.UserType type, int time);

public Guid VerifyTokenAndGetId(string token);



}


