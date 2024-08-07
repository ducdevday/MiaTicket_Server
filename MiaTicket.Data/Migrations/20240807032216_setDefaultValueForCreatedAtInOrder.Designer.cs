﻿// <auto-generated />
using System;
using MiaTicket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiaTicket.Data.Migrations
{
    [DbContext(typeof(MiaTicketDBContext))]
    [Migration("20240807032216_setDefaultValueForCreatedAtInOrder")]
    partial class setDefaultValueForCreatedAtInOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiaTicket.Data.Entity.Banner", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("Banner", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressDistinct")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressName")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressNo")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressProvince")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressWard")
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BackgroundUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsOffline")
                        .HasColumnType("bit");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrganizerInformation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrganizerLogoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrganizerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PaymentAccount")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PaymentBankBranch")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentBankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressDistinct")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressNo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressProvince")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AddressWard")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BackgroundUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("GETDATE()")
                        .HasAnnotation("Timestamp", "CreatedDate");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsOffline")
                        .HasColumnType("bit");

                    b.Property<decimal>("IsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrganizerInformation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrganizerLogoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrganizerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<string>("PaymentStatusId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<string>("PaymentTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QrCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QrUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.OrderTicket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrderTicketStatisId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderTicketStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<double>("Price")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderTicket");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.ShowTime", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("ShowTime", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("MaximumPurchase")
                        .HasColumnType("int");

                    b.Property<int>("MininumPurchase")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SaleStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShowTimeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ShowTimeId");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("UserStatusId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Voucher", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("bit");

                    b.Property<int?>("MaxQuanityPerOrder")
                        .HasColumnType("int");

                    b.Property<int?>("MinQuanityPerOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Voucher", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.VoucherFixedAmount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.Property<string>("VoucherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId")
                        .IsUnique();

                    b.ToTable("VoucherFixedAmount", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.VoucherPercentage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<string>("VoucherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId")
                        .IsUnique();

                    b.ToTable("VoucherPercentage", (string)null);
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Banner", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Event", "Event")
                        .WithOne("Banner")
                        .HasForeignKey("MiaTicket.Data.Entity.Banner", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Event", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Category", "Category")
                        .WithOne("Event")
                        .HasForeignKey("MiaTicket.Data.Entity.Event", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiaTicket.Data.Entity.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Order", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Event", "Event")
                        .WithOne("Order")
                        .HasForeignKey("MiaTicket.Data.Entity.Order", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiaTicket.Data.Entity.User", "User")
                        .WithOne("Order")
                        .HasForeignKey("MiaTicket.Data.Entity.Order", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.OrderTicket", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Order", "Order")
                        .WithMany("OrderTickets")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.ShowTime", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Event", "Event")
                        .WithMany("ShowTimes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Ticket", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.ShowTime", "ShowTime")
                        .WithMany("Tickets")
                        .HasForeignKey("ShowTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShowTime");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Voucher", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Event", "Event")
                        .WithMany("Vouchers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.VoucherFixedAmount", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Voucher", "Voucher")
                        .WithOne("VoucherFixedAmount")
                        .HasForeignKey("MiaTicket.Data.Entity.VoucherFixedAmount", "VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.VoucherPercentage", b =>
                {
                    b.HasOne("MiaTicket.Data.Entity.Voucher", "Voucher")
                        .WithOne("VoucherPercentage")
                        .HasForeignKey("MiaTicket.Data.Entity.VoucherPercentage", "VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Category", b =>
                {
                    b.Navigation("Event")
                        .IsRequired();
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Event", b =>
                {
                    b.Navigation("Banner");

                    b.Navigation("Order")
                        .IsRequired();

                    b.Navigation("ShowTimes");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Order", b =>
                {
                    b.Navigation("OrderTickets");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.ShowTime", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("MiaTicket.Data.Entity.Voucher", b =>
                {
                    b.Navigation("VoucherFixedAmount");

                    b.Navigation("VoucherPercentage");
                });
#pragma warning restore 612, 618
        }
    }
}
