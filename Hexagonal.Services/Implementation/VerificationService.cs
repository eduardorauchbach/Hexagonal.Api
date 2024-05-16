using Hexagonal.Common.Configurations;
using Hexagonal.Common.Constants;
using Hexagonal.Common.DTO;
using Hexagonal.Domain.Entities.Verifications;
using Hexagonal.DTOs.Request.Verifications;
using Hexagonal.DTOs.Response.Verifications;
using Hexagonal.Repositories;
using Microsoft.Extensions.Options;
using RauchTech.Logging;
using RauchTech.Logging.Aspects;

namespace Hexagonal.Services.Implementation
{
    internal class VerificationService : IVerificationService
    {
        private readonly ICustomLog<VerificationService> _log;

        private readonly AppSettings _appSettings;

        private readonly IVerificationRepository _verificationRepository;

        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public VerificationService(
            ICustomLog<VerificationService> log,
            IOptions<AppSettings> options,
            IVerificationRepository verificationRepository,
            IHashService hashService,
            ITokenService tokenService
        )
        {
            _log = log;
            _appSettings = options.Value;

            _hashService = hashService;
            _tokenService = tokenService;
            _verificationRepository = verificationRepository;
        }

        [LogAspect]
        public async Task<Result<DTOVerificationCreateResponse>> Create(DTOVerificationCreateRequest request)
        {
            var code = GenerateRandomString();

            var verification = request.ToDomain();

            verification.NewId();
            verification.Value = _hashService.HashValue(code);
            verification.ExpiresAt = DateTime.UtcNow.AddMinutes(_appSettings.VerificationTimeout);

            await _verificationRepository.Create(verification);
            if (!_appSettings.VerificationMock)
            {
                switch (verification.Type)
                {
                    case VerificationType.Email:
                        {
                            //Email with the code logic, 
                        }
                        break;
                }
            }

            return Result.Success(verification.ToDTOResponse());
        }

        [LogAspect]
        public async Task<Result<DTOVerificationValidateResponse>> Validate(DTOVerificationValidateRequest request)
        {
            var verification = await _verificationRepository.Get(request.Id);

            if (verification == null)
            {
                Result.Failure(Messages.VerificationNotFound, System.Net.HttpStatusCode.NotFound);
            }

            if (verification.ExpiresAt < DateTime.UtcNow)
            {
                Result.Failure(Messages.VerificationExpired, System.Net.HttpStatusCode.NotAcceptable);
            }

            var success = _appSettings.VerificationMock || _hashService.VerifyValue(request.Value, verification.Value);
            var result = new DTOVerificationValidateResponse
            {
                Success = success,
                Token = success ? _tokenService.GenerateAnonymousToken(verification.Origin) : null
            };

            return Result.Success(result);
        }

        #region [Auxiliar]

        private static Random random = new();
        private static string GenerateRandomString()
        {
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result += random.Next(0, 10).ToString(); // Gera um número entre 0 e 9
            }
            return result;
        }

        #endregion
    }
}
