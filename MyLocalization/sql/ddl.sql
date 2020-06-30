create table localization_resource(
  key_name   varchar(20),
  ja_jp      varchar(200),
  en_us      varchar(200),
  constraint pk_localization_resource primary key(
    key_name
  )
);
