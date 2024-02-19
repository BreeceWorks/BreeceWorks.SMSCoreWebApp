using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreeceWorks.Shared.Migrations.CommunicationDb
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyPhoneNumbers",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SMSProcessor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPhoneNumbers", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    OptStatus = table.Column<bool>(type: "bit", nullable: false),
                    OptStatusDetail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lineOfBusinesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lineOfBusinesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MessageAuthors",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAuthors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplates",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "OperatorRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    ClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deductible = table.Column<int>(type: "int", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineOfBusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CaseType = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryContact = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Privacy = table.Column<int>(type: "int", nullable: true),
                    SMSNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguagePreference = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_lineOfBusinesses_LineOfBusinessId",
                        column: x => x.LineOfBusinessId,
                        principalTable: "lineOfBusinesses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplateValues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTemplateDtoTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplateValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageTemplateValues_MessageTemplates_MessageTemplateDtoTemplateId",
                        column: x => x.MessageTemplateDtoTemplateId,
                        principalTable: "MessageTemplates",
                        principalColumn: "TemplateId");
                });

            migrationBuilder.CreateTable(
                name: "OperatorDtoOperatorRoleDto",
                columns: table => new
                {
                    OperatorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorDtoOperatorRoleDto", x => new { x.OperatorsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_OperatorDtoOperatorRoleDto_OperatorRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "OperatorRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperatorDtoOperatorRoleDto_Operators_OperatorsId",
                        column: x => x.OperatorsId,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseDtoOperatorDto",
                columns: table => new
                {
                    CasesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecondaryOperatorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDtoOperatorDto", x => new { x.CasesId, x.SecondaryOperatorsId });
                    table.ForeignKey(
                        name: "FK_CaseDtoOperatorDto_Cases_CasesId",
                        column: x => x.CasesId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseDtoOperatorDto_Operators_SecondaryOperatorsId",
                        column: x => x.SecondaryOperatorsId,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    sMSId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<int>(type: "int", nullable: false),
                    formatting = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    channelSource = table.Column<int>(type: "int", nullable: false),
                    authorid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    needsAttention = table.Column<bool>(type: "bit", nullable: false),
                    needsAction = table.Column<bool>(type: "bit", nullable: false),
                    messageTemplateTemplateId = table.Column<int>(type: "int", nullable: true),
                    CaseDtoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_Messages_Cases_CaseDtoId",
                        column: x => x.CaseDtoId,
                        principalTable: "Cases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_MessageAuthors_authorid",
                        column: x => x.authorid,
                        principalTable: "MessageAuthors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Messages_MessageTemplates_messageTemplateTemplateId",
                        column: x => x.messageTemplateTemplateId,
                        principalTable: "MessageTemplates",
                        principalColumn: "TemplateId");
                });

            migrationBuilder.CreateTable(
                name: "MessageAttachmentDtoMessageDto",
                columns: table => new
                {
                    Messagesid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    messageAttachmentsid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachmentDtoMessageDto", x => new { x.Messagesid, x.messageAttachmentsid });
                    table.ForeignKey(
                        name: "FK_MessageAttachmentDtoMessageDto_MessageAttachments_messageAttachmentsid",
                        column: x => x.messageAttachmentsid,
                        principalTable: "MessageAttachments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageAttachmentDtoMessageDto_Messages_Messagesid",
                        column: x => x.Messagesid,
                        principalTable: "Messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_CaseDtoOperatorDto_SecondaryOperatorsId",
                table: "CaseDtoOperatorDto",
                column: "SecondaryOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CustomerId",
                table: "Cases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_LineOfBusinessId",
                table: "Cases",
                column: "LineOfBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachmentDtoMessageDto_messageAttachmentsid",
                table: "MessageAttachmentDtoMessageDto",
                column: "messageAttachmentsid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_authorid",
                table: "Messages",
                column: "authorid");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CaseDtoId",
                table: "Messages",
                column: "CaseDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_messageTemplateTemplateId",
                table: "Messages",
                column: "messageTemplateTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplateValues_MessageTemplateDtoTemplateId",
                table: "MessageTemplateValues",
                column: "MessageTemplateDtoTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatorDtoOperatorRoleDto_RolesId",
                table: "OperatorDtoOperatorRoleDto",
                column: "RolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseDtoOperatorDto");

            migrationBuilder.DropTable(
                name: "CompanyPhoneNumbers");

            migrationBuilder.DropTable(
                name: "MessageAttachmentDtoMessageDto");

            migrationBuilder.DropTable(
                name: "MessageTemplateValues");

            migrationBuilder.DropTable(
                name: "OperatorDtoOperatorRoleDto");

            migrationBuilder.DropTable(
                name: "MessageAttachments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OperatorRoles");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "MessageAuthors");

            migrationBuilder.DropTable(
                name: "MessageTemplates");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "lineOfBusinesses");
        }
    }
}
