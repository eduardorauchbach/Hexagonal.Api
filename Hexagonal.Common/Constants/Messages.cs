using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Common.Constants
{
    public static class Messages
    {
        public const string GenericInvalidData = "Dados inválidos!";
        public const string GenericSuccess = "Operação realizada com sucesso!";
        public const string GenericFailed = "Operação não realizada!";
        public const string GenericNotAllowed = "Ação não permitida!";
        public const string GenericNotAllowedForUser = "Ação não permitida para este usuário!";

        public const string TokenEmailValidationFailed = "Email inválido para o token";

        public const string ProfileNotFound = "Perfil não encontrado!";
        public const string ProfileAlreadyExists = "Perfil já cadastrado!";
        public const string ProfileNameAlreadyExists = "Nome de perfil já cadastrado!";

        public const string UserNotFound = "Usuário não encontrado!";
        public const string UserAlreadyExists = "Usuário já cadastrado!";
        public const string UserOrPasswordInvalid = "Usuário ou senha inválido!";

        public const string VerificationNotFound = "Verificação não encontrada!";
        public const string VerificationExpired = "Verificação não encontrada!";
        public const string VerificationFailed = "Número de verificação inválido!";
    }
}
