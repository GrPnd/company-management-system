﻿// <auto-generated />
using System;
using App.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.DAL.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("App.Domain.Absence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AuthorizedByUserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ByUserId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorizedByUserId");

                    b.HasIndex("ByUserId");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("App.Domain.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("App.Domain.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("App.Domain.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("App.Domain.Meeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMandatory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("App.Domain.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FromUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ToUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("App.Domain.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("App.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("App.Domain.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("App.Domain.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("App.Domain.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AssignedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserInTeamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserInTeamId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("App.Domain.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("App.Domain.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FromUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ToUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("App.Domain.UserInRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Since")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Until")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersInRoles");
                });

            modelBuilder.Entity("App.Domain.UserInTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Since")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Until")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersInTeams");
                });

            modelBuilder.Entity("App.Domain.UserInTeamInTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Review")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Since")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Until")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserInTeamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserInTeamId");

                    b.ToTable("UsersInTeamsInTasks");
                });

            modelBuilder.Entity("App.Domain.UserInWorkDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Since")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Until")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WorkDayId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkDayId");

                    b.ToTable("UsersInWorkDays");
                });

            modelBuilder.Entity("App.Domain.WorkDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Day")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("WorkDays");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("App.Domain.Absence", b =>
                {
                    b.HasOne("App.Domain.Person", "AuthorizedByUser")
                        .WithMany("AuthorizedByAbsences")
                        .HasForeignKey("AuthorizedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Person", "ByUser")
                        .WithMany("ByAbsences")
                        .HasForeignKey("ByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorizedByUser");

                    b.Navigation("ByUser");
                });

            modelBuilder.Entity("App.Domain.Meeting", b =>
                {
                    b.HasOne("App.Domain.Team", "Team")
                        .WithMany("Meetings")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("App.Domain.Message", b =>
                {
                    b.HasOne("App.Domain.Person", "FromUser")
                        .WithMany("FromMessages")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Person", "ToUser")
                        .WithMany("ToMessages")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("App.Domain.Person", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", "User")
                        .WithMany("Persons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domain.Role", b =>
                {
                    b.HasOne("App.Domain.Person", null)
                        .WithMany("Roles")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("App.Domain.Schedule", b =>
                {
                    b.HasOne("App.Domain.Team", "Team")
                        .WithMany("Schedules")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("App.Domain.Task", b =>
                {
                    b.HasOne("App.Domain.Status", "Status")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.UserInTeam", "UserInTeam")
                        .WithMany("Tasks")
                        .HasForeignKey("UserInTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("UserInTeam");
                });

            modelBuilder.Entity("App.Domain.Team", b =>
                {
                    b.HasOne("App.Domain.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("App.Domain.Ticket", b =>
                {
                    b.HasOne("App.Domain.Person", "FromUser")
                        .WithMany("FromTickets")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Person", "ToUser")
                        .WithMany("ToTickets")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("App.Domain.UserInRole", b =>
                {
                    b.HasOne("App.Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Person", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domain.UserInTeam", b =>
                {
                    b.HasOne("App.Domain.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Person", "User")
                        .WithMany("UserInTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domain.UserInTeamInTask", b =>
                {
                    b.HasOne("App.Domain.Task", "Task")
                        .WithMany("UserInTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.UserInTeam", "UserInTeam")
                        .WithMany("UserInTeamInTasks")
                        .HasForeignKey("UserInTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");

                    b.Navigation("UserInTeam");
                });

            modelBuilder.Entity("App.Domain.UserInWorkDay", b =>
                {
                    b.HasOne("App.Domain.Person", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.WorkDay", "WorkDay")
                        .WithMany("UsersInWorkDay")
                        .HasForeignKey("WorkDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkDay");
                });

            modelBuilder.Entity("App.Domain.WorkDay", b =>
                {
                    b.HasOne("App.Domain.Person", null)
                        .WithMany("WorkDays")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("App.Domain.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Domain.Identity.AppUser", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("App.Domain.Person", b =>
                {
                    b.Navigation("AuthorizedByAbsences");

                    b.Navigation("ByAbsences");

                    b.Navigation("FromMessages");

                    b.Navigation("FromTickets");

                    b.Navigation("Roles");

                    b.Navigation("ToMessages");

                    b.Navigation("ToTickets");

                    b.Navigation("UserInTeams");

                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("App.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("App.Domain.Status", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("App.Domain.Task", b =>
                {
                    b.Navigation("UserInTasks");
                });

            modelBuilder.Entity("App.Domain.Team", b =>
                {
                    b.Navigation("Meetings");

                    b.Navigation("Schedules");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("App.Domain.UserInTeam", b =>
                {
                    b.Navigation("Tasks");

                    b.Navigation("UserInTeamInTasks");
                });

            modelBuilder.Entity("App.Domain.WorkDay", b =>
                {
                    b.Navigation("UsersInWorkDay");
                });
#pragma warning restore 612, 618
        }
    }
}
