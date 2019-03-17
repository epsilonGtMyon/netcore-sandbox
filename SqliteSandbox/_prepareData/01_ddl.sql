create table app_log(
  id integer
 ,log_text
 ,constraint pk_app_log  primary key(
   id
 )
)

------------------------

create table juchu(
  juchu_no       text
 ,juchu_edno     integer
 ,product_name   text
 ,product_count  integer
 ,constraint pk_juchu primary key(
    juchu_no
   ,juchu_edno
 )
)
