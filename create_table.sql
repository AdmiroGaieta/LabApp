﻿CREATE TABLE IF NOT EXISTS Schools (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL,
    NumberOfClassrooms INTEGER NOT NULL,
    Province TEXT NOT NULL
);
