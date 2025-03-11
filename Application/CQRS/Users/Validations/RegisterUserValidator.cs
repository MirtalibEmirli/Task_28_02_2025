

using Common.Security;
using Domain.Enums;
using FluentValidation;
using System.Net;
using System.Xml.Linq;
using System.Xml;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.CQRS.Users.Validations
{
    public class RegisterUserValidator:AbstractValidator<RegisterCommand>
    {

        //validationlari oyren
        //user in diger crudlari ve bookun diger crudlari yazilmalidir
        //backgroundserver mail gondersin
        //requuest rate limit Middleware
        //Postman bax
        //JWT bax ve login yaz



        public RegisterUserValidator()
        {

            //RuleFor(u => u.Email).EmailAddress();
            //RuleFor(u => u.Name).NotEmpty().MinimumLength(3).NotNull();
            //RuleFor(u => u.Surname).NotEmpty().MinimumLength(3).NotNull();
            //RuleFor(u => u.CardNumber).NotEmpty().MinimumLength(16).NotNull().MaximumLength(16).CreditCard();
            //RuleFor(u => u.PasswordHash).NotEmpty().MinimumLength(4);
            //RuleFor(u => u.MobilePhone).NotEmpty().NotNull().Must(phone => phone.StartsWith("+994")).WithMessage("Nomre +994 ile baslamalidir");
        }
    }
}

//CREATE TABLE[dbo].[Users] (
//    [Id]               INT IDENTITY(1, 1) NOT NULL,
//    [Name]             NVARCHAR (100) NOT NULL,
//    [Surname]          NVARCHAR (100) NOT NULL,
//    [Username]         NVARCHAR (100) NOT NULL,
//    [FatherName]       NVARCHAR (100) NULL,
//    [Email]            NVARCHAR (255) NOT NULL,
//    [PasswordHash]     NVARCHAR (500) NOT NULL,
//    [Address]          NVARCHAR (255) NULL,
//    [MobilePhone]      NVARCHAR (20)  NULL,
//    [CardNumber]       NVARCHAR (16)  NULL,
//    [TableNumber]      NVARCHAR (50)  NULL,
//    [Birthdate]        DATETIME2 (7)  NOT NULL,
//    [DateOfEmployment] DATE           NULL,
//    [DateOfDismissal]  DATE           NULL,
//    [Note]             NVARCHAR (500) NULL,
//    [Gender]           INT            NOT NULL,
//    [UserType]         INT            NOT NULL,
//    [CreatedBy]        INT            NULL,
//    [UpdatedBy]        INT            NULL,
//    [DeletedBy]        INT            NULL,
//    [CreatedDate]      DATETIME       DEFAULT (getdate()) NOT NULL,
//    [UpdatedDate]      DATETIME       NULL,
//    [DeletedDate]      DATETIME       NULL,
//    [IsDeleted]        BIT            DEFAULT ((0)) NOT NULL,
//    [ImageId]          INT            NULL,
//    [ImageUrl]         NVARCHAR (250) NULL,
//    PRIMARY KEY CLUSTERED ([Id] ASC),
//    UNIQUE NONCLUSTERED([Username] ASC),
//    FOREIGN KEY([ImageId]) REFERENCES[dbo].[Images]([Id]),
//    CHECK([Email] LIKE '%@%'),
//    CHECK([MobilePhone] LIKE '+994%'),
//    CHECK(len([CardNumber]) = (16))
//);



