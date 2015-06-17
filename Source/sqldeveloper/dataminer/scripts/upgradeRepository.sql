WHENEVER SQLERROR EXIT SQL.SQLCODE;
EXECUTE dbms_output.put_line('');
EXECUTE dbms_output.put_line('Start Data Miner Repository DB Object Upgrade.');
EXECUTE dbms_output.put_line('');
@@upgradeRepo11_2_0_1_10To11_2_0_1_11.sql
@@upgradeRepo11_2_0_1_11To11_2_0_1_12.sql
@@upgradeRepo11_2_0_1_12To11_2_0_1_13.sql
@@upgradeRepo11_2_0_1_13To11_2_1_1_1.sql
@@upgradeRepo11_2_1_1_1To11_2_1_1_2.sql
@@upgradeRepo11_2_1_1_2To11_2_1_1_3.sql
@@upgradeRepo11_2_1_1_3To11_2_1_1_4.sql
@@upgradeRepo11_2_1_1_4To11_2_1_1_5.sql
@@upgradeRepo11_2_1_1_5To11_2_1_1_6.sql
EXECUTE dbms_output.put_line('');
EXECUTE dbms_output.put_line('End Data Miner Repository DB Object Upgrade.');
EXECUTE dbms_output.put_line('');
