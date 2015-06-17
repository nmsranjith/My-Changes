-- validateVersionUpgrade.sql
-- validate that the repository version and workflow schema version have
-- successfully been updated to their target version numbers
-- If this has not happened, then exit with error
-- If the version parameters pass in have the value 'SKIP', then skip that version test
-- Example:
-- Parameter 1: Repository Version Number
-- Parameter 2: Worfklow Schema Version Number
-- @validateVersionUpgrade.sql '11.2.2.1.1' '11.2.2.1.1'
-- @validateVersionUpgrade.sql '11.2.2.1.1' 'SKIP'

SET serveroutput ON
SET verify OFF
DEFINE REPOSITORY_VERSION = &&1
DEFINE WF_SCHEMA_VERSION  = &&2
--DEFINE REPOSITORY_VERSION = '11.2.2.1.1' -- for testing
--DEFINE WF_SCHEMA_VERSION  = '11.2.2.1.1' -- for testing
EXECUTE dbms_output.put_line('');
EXECUTE dbms_output.put_line('Validate that the Repository Version and Workflow Version have beend updated.');
EXECUTE dbms_output.put_line('');
DECLARE
  w_version_type        VARCHAR2(100) := '';
  w_version_status      VARCHAR2(100) := '';
  w_repo_cur_version    VARCHAR2(100) := '';
  w_repo_cur_wf_version VARCHAR2(100) := '';
  w_load_failed         BOOLEAN       := FALSE;
  w_db_ver              VARCHAR2(30);
  w_patch               VARCHAR2(30);
  w_check_schema        BOOLEAN;
  --select property_name, property_str_value
  --from ODMRSYS.ODMR$REPOSITORY_PROPERTIES
  --where property_name in ('VERSION','WF_VERSION');
  CURSOR version_cursor
  IS
    SELECT property_name,
      property_str_value
    FROM ODMRSYS.ODMR$REPOSITORY_PROPERTIES
    WHERE property_name IN ('VERSION','WF_VERSION');
BEGIN
  DBMS_OUTPUT.ENABLE(NULL);
  SELECT version
  INTO w_db_ver
  FROM product_component_version
  WHERE product LIKE 'Oracle Database%';
  IF (INSTR(w_db_ver, '11.2.0.2') > 0 OR INSTR(w_db_ver, '11.2.0.1') > 0 OR INSTR(w_db_ver, '11.2.0.0') > 0) THEN
    BEGIN
      SELECT PROPERTY_STR_VALUE
      INTO w_patch
      FROM ODMRSYS.ODMR$REPOSITORY_PROPERTIES
      WHERE PROPERTY_NAME = 'MAINTAIN_DOM_PATCH_INSTALLED';
    EXCEPTION
    WHEN NO_DATA_FOUND THEN
      w_patch := 'FALSE';
    END;
    IF (w_patch       = 'TRUE') THEN
      w_check_schema := TRUE;
    ELSE
      w_check_schema := FALSE;
      dbms_output.put_line('No workflow schema migration for database version 11.2.0.2.0 and older without the maintain dom patch');
    END IF;
  ELSE
    w_check_schema := TRUE; -- allow migration if database >= 11.2.0.3.0
  END IF;
  if (w_check_schema = TRUE) THEN
    DBMS_OUTPUT.PUT_LINE ('Workflow Schema version must match');
  else
    DBMS_OUTPUT.PUT_LINE ('Workflow Schema version is allowed not to match as the db version can not upgrade xml schema');
  END IF;
  OPEN version_cursor;
  FETCH version_cursor INTO w_version_type,w_version_status;
  WHILE version_cursor%FOUND
  LOOP
    IF (w_version_type = 'VERSION') THEN
      DBMS_OUTPUT.PUT_LINE ('Current Repository Version: ' || w_version_type || ' Status: ' || w_version_status);
      w_repo_cur_version := w_version_status;
    ELSE
      DBMS_OUTPUT.PUT_LINE ('Current Workflow Schema Version: ' || w_version_type || ' Status: ' || w_version_status);
      w_repo_cur_wf_version := w_version_status;
    END IF;
    FETCH version_cursor INTO w_version_type,w_version_status;
  END LOOP;
  CLOSE version_cursor;
  DBMS_OUTPUT.PUT_LINE ('Target Repository Version: VERSION  Status: ' || '&REPOSITORY_VERSION');
  DBMS_OUTPUT.PUT_LINE ('Target Workflow Schema Version: WF_VERSION Status: ' || '&WF_SCHEMA_VERSION');
  -- skip check if version number is empty
  IF ('SKIP'                   != '&REPOSITORY_VERSION') THEN
    IF (w_repo_cur_version != '&REPOSITORY_VERSION') THEN
      w_load_failed        := TRUE;
    END IF;
  END IF;
  -- skip check if version number is empty
  IF ('SKIP'                        != '&WF_SCHEMA_VERSION') THEN
    IF (w_check_schema           = TRUE) THEN
      DBMS_OUTPUT.PUT_LINE ('Performing schema version check');
      IF (w_repo_cur_wf_version != '&WF_SCHEMA_VERSION') THEN
        w_load_failed           := TRUE;
      END IF;
    END IF;
  END IF;
  -- fail if the versions were not properly updated
  IF (w_load_failed = TRUE) THEN
    RAISE_APPLICATION_ERROR(-20000, 'The Current Repository Version and/or Workflow Version were not updated to match the Target version.  Review install log.');
  END IF;
END;
/
