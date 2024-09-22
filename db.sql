-- DDL Generated from https:/databasediagram.com

CREATE TABLE product (
  id INT PRIMARY KEY,
  name STRING,
  period DATETIME
);

CREATE TABLE key (
  id INT PRIMARY KEY,
  product_id INT REFERENCES product(id),
  key STRING,
  status STATUS,
  start_time DATETIME,
  data JSON
);