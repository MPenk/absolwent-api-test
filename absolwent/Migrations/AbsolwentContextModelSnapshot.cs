﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using absolwent.Database;

#nullable disable

namespace absolwent.Migrations
{
    [DbContext(typeof(AbsolwentContext))]
    partial class AbsolwentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("absolwent.DTO.Data", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanySize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Earnings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobSatisfaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobSearchTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeriodOfEmployment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProffesionalActivity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("QuestionareId")
                        .HasColumnType("bigint");

                    b.Property<string>("TownSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Training")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionareId");

                    b.ToTable("Data");
                });

            modelBuilder.Entity("absolwent.DTO.Graduate", b =>
                {
                    b.Property<long>("Graduate_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Graduate_id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Graduation_year")
                        .HasColumnType("int");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Graduate_id");

                    b.ToTable("Graduate");
                });

            modelBuilder.Entity("absolwent.DTO.Questionnaire", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Filled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FillingData")
                        .HasColumnType("datetime2");

                    b.Property<long>("Graduate_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SendingData")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Graduate_id");

                    b.ToTable("Questionnaire");
                });

            modelBuilder.Entity("absolwent.DTO.University", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PasswordResetDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordResetKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionnaireFrequency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("University");
                });

            modelBuilder.Entity("absolwent.DTO.Data", b =>
                {
                    b.HasOne("absolwent.DTO.Questionnaire", "Questionare")
                        .WithMany()
                        .HasForeignKey("QuestionareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionare");
                });

            modelBuilder.Entity("absolwent.DTO.Questionnaire", b =>
                {
                    b.HasOne("absolwent.DTO.Graduate", "Graduate")
                        .WithMany()
                        .HasForeignKey("Graduate_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Graduate");
                });
#pragma warning restore 612, 618
        }
    }
}
