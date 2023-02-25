﻿// <auto-generated />
using System;
using GeekShopping.Order.Api.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.Order.Api.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20230225165428_CreateOrdersTables")]
    partial class CreateOrdersTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.Order.Api.Models.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("address");

                    b.Property<string>("Coupon")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("coupon");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("customer");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("document");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("status");

                    b.Property<decimal>("Total")
                        .HasPrecision(19, 2)
                        .HasColumnType("decimal(19,2)")
                        .HasColumnName("total");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("order");
                });

            modelBuilder.Entity("GeekShopping.Order.Api.Models.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("name");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)")
                        .HasColumnName("order_id");

                    b.Property<decimal>("Price")
                        .HasPrecision(19, 2)
                        .HasColumnType("decimal(19,2)")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<decimal>("Total")
                        .HasPrecision(19, 2)
                        .HasColumnType("decimal(19,2)")
                        .HasColumnName("total");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("order_item");
                });

            modelBuilder.Entity("GeekShopping.Order.Api.Models.Entities.OrderItem", b =>
                {
                    b.HasOne("GeekShopping.Order.Api.Models.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("GeekShopping.Order.Api.Models.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
