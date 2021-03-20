using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
       public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .WithMessage("La descripcion no puede ser nula");

            RuleFor(post => post.Description)
                   .Length(8, 500)
                   .WithMessage("La logitud debe pasar los 8 caracteres ");

            /*RuleFor(post => post.Description)
                 .NotNull()
                 .Length(8, 500);*/


            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);

        }
    }
}
