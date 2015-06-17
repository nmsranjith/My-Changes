--    DESCRIPTION
--      This script grants SELECT on SH tables 
--      required to support Data Miner Cue Card demos
--      
--      The script is to be run in SYS account
--    NOTES
--       &&1    Name of the DM user
--    MODIFIED   (MM/DD/YY)
--       mbkelly     07/31/10 - copied and revised from rbms/demo to support
--                               Data Miner 
-- Example:
-- @dmshgrants.sql DMUSER
--------------------------------------------------------------------------------

DEFINE USER_ACCOUNT = &&1 

EXECUTE dbms_output.put_line('');
EXECUTE dbms_output.put_line('Grant access to SH schema objects.');
EXECUTE dbms_output.put_line('');

-- unlock SH Account (MK:prompts for new password so I have commented this out)
--PASSWORD SH UNLOCK;

GRANT SELECT ON sh.customers TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.sales TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.products TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.supplementary_demographics TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.countries TO "&USER_ACCOUNT"
/ 
GRANT SELECT ON sh.cal_month_sales_mv TO "&USER_ACCOUNT"
/ 
GRANT SELECT ON sh.channels TO "&USER_ACCOUNT"
/ 
GRANT SELECT ON sh.costs TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.FWEEK_PSCAT_SALES_MV TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.promotions TO "&USER_ACCOUNT"
/
GRANT SELECT ON sh.times TO "&USER_ACCOUNT"
/