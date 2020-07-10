create table localization_resource(
  key_name   varchar(20),
  ja         varchar(200),
  en         varchar(200),
  constraint pk_localization_resource primary key(
    key_name
  )
);

comment on table  localization_resource          is '国際化リソース';
comment on column localization_resource.key_name is 'キー名';
comment on column localization_resource.ja       is '日本語';
comment on column localization_resource.en       is '英語';

