﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using FileInfo = Data.Entities.FileInfo;

namespace Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<FileData> FileDatas { get; set; }
    public DbSet<FileInfo> FileInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileInfo>(
            entity =>
            {
                entity.ToTable("FileInfo");
            });

        modelBuilder.Entity<FileData>(
            entity =>
            {
                entity.ToTable("FileData");
            });
    }
}