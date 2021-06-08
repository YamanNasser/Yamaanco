using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.Enums
{
    public enum YamaancoHttpStatusCode
    {
        RequestedUserIsMemberOfGroup = 700,
        UserNameAlreadyExist = 701,
        UserEmailAlreadyExist = 702,
        UserPhoneNumberAlreadyExist = 702,
        NotConfirmedAccount = 703,
        InvalidCredentials = 704,
    }
}
