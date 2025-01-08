using AuthenticatorDEV.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    //GET: api/Users
    [HttpPost]
    public IActionResult Post([FromBody] User usr)
    {
        bool Verified = getValidation(usr);
        return Ok(new string[] {"Verified", Verified.ToString()});
    }

    private bool getValidation(User usr)
    {
        bool validation = false;

        if((usr.Email == "testme@test.com")
        && (usr.Password == "insecurePWD"))
        {
            validation = true;
        }
        return validation;
    }
}