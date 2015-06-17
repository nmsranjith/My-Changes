--------------------------------------------------------------------------------
--    DESCRIPTION
--      This script creates views using SH data
--      in the schema of the data mining user.
--      Some of these are referenced by Data Miner Cue Card Demos.
--      Others are referenced for use by demo programs.
--      This is not an exact copy of the dmsh.sql used in the 
--      supporting demo programs. Specifically, no text indexing
--      is being performed and no nested tables are being generated.
--      Data Miner adresses these steps differently then do the demo
--      programs.
--
--    NOTES
--       The script assumes that the full SH schema is already created and the
--       necessary SELECTs have been granted (See dmshgrants.sql). This script runs in 
--       the schema of the data mining user.
--       mining_data_*_v views : Used for mining 
--
--    MODIFIED   (MM/DD/YY)
--       mbkelly     07/31/10 - copied from rdbms/demo and revised for ODMr
--
--    Parameters:
--    1. User Account - user account to create the views in
--    Example:
--    @dmsh.sql DMUSER
--------------------------------------------------------------------------------
--

-- User Account subsitution variable
DEFINE USER_ACCOUNT = &&1

ALTER session set current_schema = "&USER_ACCOUNT";

EXECUTE dbms_output.put_line('');
EXECUTE dbms_output.put_line('Create Data Miner demo tables and views based on SH schema.');
EXECUTE dbms_output.put_line('');

-- Build , test and apply views, no text
CREATE VIEW "&USER_ACCOUNT".mining_data_apply_v AS
SELECT
 a.CUST_ID,
 a.CUST_GENDER,
 2003-a.CUST_YEAR_OF_BIRTH AGE,
 a.CUST_MARITAL_STATUS,
 c.COUNTRY_NAME,
 a.CUST_INCOME_LEVEL,
 b.EDUCATION,
 b.OCCUPATION,
 b.HOUSEHOLD_SIZE,
 b.YRS_RESIDENCE,
 b.AFFINITY_CARD,
 b.BULK_PACK_DISKETTES,
 b.FLAT_PANEL_MONITOR,
 b.HOME_THEATER_PACKAGE,
 b.BOOKKEEPING_APPLICATION,
 b.PRINTER_SUPPLIES,
 b.Y_BOX_GAMES,
 b.OS_DOC_SET_KANJI
FROM
 sh.customers a,
 sh.supplementary_demographics b,
 sh.countries c
WHERE
 a.CUST_ID = b.CUST_ID
 AND a.country_id  = c.country_id
 AND a.cust_id between 100001 and 101500;
 
CREATE VIEW "&USER_ACCOUNT".mining_data_build_v AS
SELECT
 a.CUST_ID,
 a.CUST_GENDER,
 2003-a.CUST_YEAR_OF_BIRTH AGE,
 a.CUST_MARITAL_STATUS,
 c.COUNTRY_NAME,
 a.CUST_INCOME_LEVEL,
 b.EDUCATION,
 b.OCCUPATION,
 b.HOUSEHOLD_SIZE,
 b.YRS_RESIDENCE,
 b.AFFINITY_CARD,
 b.BULK_PACK_DISKETTES,
 b.FLAT_PANEL_MONITOR,
 b.HOME_THEATER_PACKAGE,
 b.BOOKKEEPING_APPLICATION,
 b.PRINTER_SUPPLIES,
 b.Y_BOX_GAMES,
 b.OS_DOC_SET_KANJI
FROM
 sh.customers a,
 sh.supplementary_demographics b,
 sh.countries c
WHERE
 a.CUST_ID = b.CUST_ID
 AND a.country_id  = c.country_id
 AND a.cust_id between 101501 and 103000;
 
CREATE VIEW "&USER_ACCOUNT".mining_data_test_v AS
SELECT
 a.CUST_ID,
 a.CUST_GENDER,
 2003-a.CUST_YEAR_OF_BIRTH AGE,
 a.CUST_MARITAL_STATUS,
 c.COUNTRY_NAME,
 a.CUST_INCOME_LEVEL,
 b.EDUCATION,
 b.OCCUPATION,
 b.HOUSEHOLD_SIZE,
 b.YRS_RESIDENCE,
 b.AFFINITY_CARD,
 b.BULK_PACK_DISKETTES,
 b.FLAT_PANEL_MONITOR,
 b.HOME_THEATER_PACKAGE,
 b.BOOKKEEPING_APPLICATION,
 b.PRINTER_SUPPLIES,
 b.Y_BOX_GAMES,
 b.OS_DOC_SET_KANJI
FROM
 sh.customers a,
 sh.supplementary_demographics b,
 sh.countries c
WHERE
 a.CUST_ID = b.CUST_ID
 AND a.country_id  = c.country_id
 AND a.cust_id between 103001 and 104500;


-- Build , test and apply views, with text
CREATE VIEW "&USER_ACCOUNT".mining_data_text_apply_v AS
SELECT
 a.CUST_ID,
 a.CUST_GENDER,
 2003-a.CUST_YEAR_OF_BIRTH AGE,
 a.CUST_MARITAL_STATUS,
 c.COUNTRY_NAME,
 a.CUST_INCOME_LEVEL,
 b.EDUCATION,
 b.OCCUPATION,
 b.HOUSEHOLD_SIZE,
 b.YRS_RESIDENCE,
 b.AFFINITY_CARD,
 b.BULK_PACK_DISKETTES,
 b.FLAT_PANEL_MONITOR,
 b.HOME_THEATER_PACKAGE,
 b.BOOKKEEPING_APPLICATION,
 b.PRINTER_SUPPLIES,
 b.Y_BOX_GAMES,
 b.OS_DOC_SET_KANJI,
 b.comments 
FROM
 sh.customers a,
 sh.supplementary_demographics b,
 sh.countries c
WHERE
 a.CUST_ID = b.CUST_ID
 AND a.country_id  = c.country_id
 AND a.cust_id between 100001 and 101500;
 
CREATE VIEW "&USER_ACCOUNT".mining_data_text_build_v AS
SELECT
 a.CUST_ID,
 a.CUST_GENDER,
 2003-a.CUST_YEAR_OF_BIRTH AGE,
 a.CUST_MARITAL_STATUS,
 c.COUNTRY_NAME,
 a.CUST_INCOME_LEVEL,
 b.EDUCATION,
 b.OCCUPATION,
 b.HOUSEHOLD_SIZE,
 b.YRS_RESIDENCE,
 b.AFFINITY_CARD,
 b.BULK_PACK_DISKETTES,
 b.FLAT_PANEL_MONITOR,
 b.HOME_THEATER_PACKAGE,
 b.BOOKKEEPING_APPLICATION,
 b.PRINTER_SUPPLIES,
 b.Y_BOX_GAMES,
 b.OS_DOC_SET_KANJI,
 b.comments 
FROM
 sh.customers a,
 sh.supplementary_demographics b,
 sh.countries c
WHERE
 a.CUST_ID = b.CUST_ID
 AND a.country_id  = c.country_id
 AND a.cust_id between 101501 and 103000;
 
CREATE VIEW "&USER_ACCOUNT".mining_data_text_test_v AS
SELECT
 a.CUST_ID,
 a.CUST_GENDER,
 2003-a.CUST_YEAR_OF_BIRTH AGE,
 a.CUST_MARITAL_STATUS,
 c.COUNTRY_NAME,
 a.CUST_INCOME_LEVEL,
 b.EDUCATION,
 b.OCCUPATION,
 b.HOUSEHOLD_SIZE,
 b.YRS_RESIDENCE,
 b.AFFINITY_CARD,
 b.BULK_PACK_DISKETTES,
 b.FLAT_PANEL_MONITOR,
 b.HOME_THEATER_PACKAGE,
 b.BOOKKEEPING_APPLICATION,
 b.PRINTER_SUPPLIES,
 b.Y_BOX_GAMES,
 b.OS_DOC_SET_KANJI,
 b.comments 
FROM
 sh.customers a,
 sh.supplementary_demographics b,
 sh.countries c
WHERE
 a.CUST_ID = b.CUST_ID
 AND a.country_id  = c.country_id
 AND a.cust_id between 103001 and 104500;

