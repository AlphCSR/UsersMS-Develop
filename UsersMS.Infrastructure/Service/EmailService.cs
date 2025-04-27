
using System.Net.Mail;
using System.Net;
using UsersMS.Core.Service;
using Microsoft.Extensions.Configuration;
using System;
using UsersMS.Core.Repositories;
using Azure.Core;
using UsersMS.Infrastructure.Exceptions;


namespace UsersMS.Infrastructure.Service
{

        public class EmailService : IEmailService
        {
            private readonly IConfiguration configuration;
            private static int VerificationCode;
            Random random = new Random();
            private readonly IAdministradorRepository _administradorRepository;
            private readonly IProveedorRepository _proveedorRepository;
            private readonly IOperadorRepository _operadorRepository;
            private readonly IConductorRepository _conductorRepository;

            public EmailService(IConfiguration configuration, IAdministradorRepository administradorRepository, IProveedorRepository proveedorRepository, IOperadorRepository operadorRepository, IConductorRepository conductorRepository)
            {
                this.configuration = configuration;
                this._administradorRepository = administradorRepository;
                this._proveedorRepository = proveedorRepository;
                this._operadorRepository = operadorRepository;
                this._conductorRepository = conductorRepository;
            }

            public async Task SendEmail(string receptor)
            {
                try
                {
                    // Verificar en el repositorio de Administradores
                    var administradorEntity = await _administradorRepository.GetByEmailAsync(receptor);
                    if (administradorEntity != null)
                    {
                        await SendVerificationEmail(receptor);
                        return;
                    }

                    // Verificar en el repositorio de Proveedores
                    var proveedorEntity = await _proveedorRepository.GetByEmailAsync(receptor);
                    if (proveedorEntity != null)
                    {
                        await SendVerificationEmail(receptor);
                        return;
                    }

                    // Verificar en el repositorio de Operadores
                    var operadorEntity = await _operadorRepository.GetByEmailAsync(receptor);
                    if (operadorEntity != null)
                    {
                        await SendVerificationEmail(receptor);
                        return;
                    }

                    // Verificar en el repositorio de Conductores
                    var conductorEntity = await _conductorRepository.GetByEmailAsync(receptor);
                    if (conductorEntity != null)
                    {
                        await SendVerificationEmail(receptor);
                        return;
                    }

                    throw new InvalidOperationException("No se encontró ninguna entidad asociada con el correo proporcionado.");
                }
                catch (SmtpException ex)
                {
                    throw new InvalidOperationException("Error al enviar el correo electrónico.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error general en el método SendEmail.", ex);
                }
            }

            private async Task SendVerificationEmail(string receptor)
            {
                var email = configuration["EMAIL_CONFIGURATION:EMAIL"];
                var password = configuration["EMAIL_CONFIGURATION:PASSWORD"];
                var host = configuration["EMAIL_CONFIGURATION:HOST"];
                var port = int.Parse(configuration["EMAIL_CONFIGURATION:PORT"]!);

                var smtpClient = new SmtpClient(host, port)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password)
                };

                var subject = "¡Tu Código de Verificación está Aquí!";
                VerificationCode = random.Next(100000, 999999);
                var body = $"<h1>Hola, {receptor}!</h1><p>Tu código de verificación es <strong>{VerificationCode}</strong>. Úsalo para completar tu proceso de recuperación de contraseña.</p><p>¡Gracias por confiar en nosotros!</p>";
                var message = new MailMessage
                {
                    From = new MailAddress(email!),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(receptor);

                await smtpClient.SendMailAsync(message);
            }

            public async Task SendPassword(string receptor, int code)
            {
                try
                {
                    // Verificar en los cuatro repositorios
                    var administradorEntity = await _administradorRepository.GetByEmailAsync(receptor);
                    if (administradorEntity != null)
                    {
                        await SendPasswordEmail(receptor, code, administradorEntity.Password!);
                        return;
                    }

                    var proveedorEntity = await _proveedorRepository.GetByEmailAsync(receptor);
                    if (proveedorEntity != null)
                    {
                        await SendPasswordEmail(receptor, code, proveedorEntity.Password!);
                        return;
                    }

                    var operadorEntity = await _operadorRepository.GetByEmailAsync(receptor);
                    if (operadorEntity != null)
                    {
                        await SendPasswordEmail(receptor, code, operadorEntity.Password!);
                        return;
                    }

                    var conductorEntity = await _conductorRepository.GetByEmailAsync(receptor);
                    if (conductorEntity != null)
                    {
                        await SendPasswordEmail(receptor, code, conductorEntity.Password!);
                        return;
                    }

                    throw new InvalidOperationException("No se encontró ninguna entidad asociada con el correo proporcionado.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Error general en el método SendPassword.", ex);
                }
            }

            private async Task SendPasswordEmail(string receptor, int code, string password)
            {
                if (VerificationCode == code)
                {
                    var email = configuration["EMAIL_CONFIGURATION:EMAIL"];
                    var emailPassword = configuration["EMAIL_CONFIGURATION:PASSWORD"];
                    var host = configuration["EMAIL_CONFIGURATION:HOST"];
                    var port = int.Parse(configuration["EMAIL_CONFIGURATION:PORT"]!);

                    var smtpClient2 = new SmtpClient(host, port)
                    {
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(email, emailPassword)
                    };

                    var subject = "¡Tu Contraseña Recuperada!";
                    var body = $"<h1>Hola, {receptor}!</h1><p>Tu contraseña ha sido recuperada exitosamente. Aquí está: <strong>{password}</strong></p><p>Por favor, cámbiala una vez que inicies sesión para asegurar tu cuenta.</p>";
                    var message = new MailMessage
                    {
                        From = new MailAddress(email!),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    message.To.Add(receptor);

                    try
                    {
                        await smtpClient2.SendMailAsync(message);
                    }
                    catch (SmtpException ex)
                    {
                        throw new InvalidOperationException("Error al enviar el correo electrónico.", ex);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("El código de verificación es incorrecto. Por favor, verifica el código e inténtalo nuevamente.");
                }
            }
        }
}





