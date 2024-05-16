using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Common.Constants
{
    public static class Messages
    {        
        public const string GenericInvalidData = "Dados inválidos";



        public const string TokenEmailValidationFailed = "Email inválido para o token";


        public const string UserNotFound = "Usuário não encontrado!";
        public const string UserAlreadyExists = "Usuário já cadastrado!";
        public const string UserOrPasswordInvalid = "Usuário ou senha inválido!";

        public const string VerificationNotFound = "Verificação não encontrada!";
        public const string VerificationExpired = "Verificação não encontrada!";
        public const string VerificationFailed = "Número de verificação inválido!";
    }
}
