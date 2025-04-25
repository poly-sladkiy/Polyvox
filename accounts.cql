CREATE KEYSPACE account_system
    WITH replication = {'class': 'SimpleStrategy', 'replication_factor' : 3};

CREATE TABLE account_system.accounts (
    account_id uuid,
    user_name text,
    email text,
    phone text,
    timestamp timestamp,
    PRIMARY KEY ((account_id), user_name, email)
) WITH comment = 'Account information';

CREATE TABLE account_system.accounts_by_email (
    email text,
    account_id uuid,
    user_name text,
    password_hash text,
    last_login_timestamp timestamp,
    failed_count_attempt int,
    PRIMARY KEY ((email), account_id, user_name, password_hash)
) WITH comment = 'Find user id in login';
