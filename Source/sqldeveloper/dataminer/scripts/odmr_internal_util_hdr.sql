
  CREATE OR REPLACE PACKAGE "ODMRSYS"."ODMR_INTERNAL_UTIL" 
AUTHID CURRENT_USER AS

  c_nest_num_name                CONSTANT VARCHAR2(30) := 'DM_NESTED_NUMERICALS';
  c_nest_cat_name                CONSTANT VARCHAR2(30) := 'DM_NESTED_CATEGORICALS';
  c_nest_num_ctyp                CONSTANT NUMBER := 100001;
  c_nest_cat_ctyp                CONSTANT NUMBER := 100002;

  -- CANONical identifier MAXimum LENgth (in bytes)
  -----------------------------------------------------------------------------
  -- This constant contains the list of string column types that are supported
  -- in this package.
  c_canon_maxlen                 CONSTANT BINARY_INTEGER := 30;

  SUBTYPE
    CIDEN_VCHAR_TYPE               IS VARCHAR2(30);

  TYPE
    CIDEN_ITAB_TYPE                IS TABLE OF CIDEN_VCHAR_TYPE
                                      INDEX BY BINARY_INTEGER;

  TYPE
    CIDEN_ATAB_TYPE                IS TABLE OF BINARY_INTEGER
                                      INDEX BY CIDEN_VCHAR_TYPE;
  TYPE
    NUM_NTAB_TYPE                  IS TABLE OF NUMBER;

  -----------------------------------------------------------------------------
  -- IDENtifier collection types
  -----------------------------------------------------------------------------
  SUBTYPE
    IDEN_VARR_TYPE                 IS DBMS_DATA_MINING_TRANSFORM.COLUMN_LIST;

  TYPE
    LSTMT_REC_TYPE                IS RECORD (
      lstmt                          dbms_sql.VARCHAR2A,
      lb                             BINARY_INTEGER DEFAULT 1,
      ub                             BINARY_INTEGER DEFAULT 0);
  --SUBTYPE
  --  LSTMT_REC_TYPE                 IS dbms_data_mining_transform.EXPRESSION_REC;

  TYPE TABLE_ARRAY                IS TABLE OF VARCHAR2(60);

  c_adt_ctyp                       CONSTANT NUMBER := 109;

  SUBTYPE
    STMT_VCHAR_TYPE                IS VARCHAR2(32767);

  FUNCTION nest_ctyp(
    p_col                          VARCHAR2,
    p_data_tref                    VARCHAR2)
  RETURN NUMBER;

  FUNCTION nest_check(
    p_col                          VARCHAR2,
    p_type                         VARCHAR2,
    p_data_tref                    VARCHAR2)
  RETURN BOOLEAN;

  PROCEDURE create_in_condition(
    p_sql_stmt      IN OUT  VARCHAR2,
    p_column_ids    IN    ODMR_OBJECT_IDS );

  PROCEDURE print_dm_transfrorm( v_transform_list IN DM_TRANSFORMS);
  PROCEDURE print_transfrorm_list( v_transform_list IN dbms_data_mining_transform.TRANSFORM_LIST);

  FUNCTION unique_object_name(
    p_prefix               VARCHAR2 DEFAULT 'ODMR$',
    p_is25bytes            BOOLEAN DEFAULT FALSE)
  RETURN VARCHAR2;

  PROCEDURE create_nested_text_table (
    p_table_name  IN VARCHAR2,
    p_src_schema  IN VARCHAR2,
    p_src_table   IN VARCHAR2,
    p_text_column IN VARCHAR2,
    p_explain_out IN VARCHAR2,
    p_case_id     IN VARCHAR2);

  FUNCTION is_table_present( p_table_name  VARCHAR2 ) RETURN BOOLEAN;
  FUNCTION is_index_present( p_index_name  VARCHAR2 ) RETURN BOOLEAN;
  FUNCTION is_key_present( p_key_name  VARCHAR2 ) RETURN BOOLEAN;

  PROCEDURE clean_table_or_view( p_object_name  VARCHAR2 );

  PROCEDURE ls_clear(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE);

  PROCEDURE ls_append(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE,
    p_txt                          VARCHAR2);

  PROCEDURE ls_append(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE,
    p_txt                          LSTMT_REC_TYPE);

  PROCEDURE ls_append(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE,
    p_txt                          CLOB);

 PROCEDURE ls_replace(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE,
    p_from_token                   VARCHAR2,
    p_to_token                     VARCHAR2,
    p_only_first                   BOOLEAN DEFAULT FALSE);

  PROCEDURE ls_desc(
    p_lstmt                        LSTMT_REC_TYPE,
    r_desc_itab                    OUT NOCOPY dbms_sql.DESC_TAB2,
    p_lf_flag                      BOOLEAN DEFAULT FALSE,
    p_lang_flag                    NUMBER DEFAULT dbms_sql.native);

  FUNCTION ls_text(
    p_lstmt             IN OUT NOCOPY LSTMT_REC_TYPE)
  RETURN VARCHAR2;

  FUNCTION ls_CLOB(
    p_lstmt             IN OUT NOCOPY LSTMT_REC_TYPE)
  RETURN CLOB;

  PROCEDURE ls_parse(
    p_cur                          NUMBER,
    p_lstmt                        LSTMT_REC_TYPE,
    p_lf_flag                      BOOLEAN DEFAULT FALSE,
    p_lang_flag                    NUMBER DEFAULT dbms_sql.native);

  PROCEDURE tref_desc(
    p_data_tref                    VARCHAR2,
    r_desc_itab                    OUT dbms_sql.DESC_TAB2);

  FUNCTION add_temp_table(
    tempTables         IN OUT NOCOPY TABLE_ARRAY,
    temp_table         IN VARCHAR2) RETURN VARCHAR2;

  PROCEDURE clean_tables_or_views(
    tempTables         IN OUT NOCOPY TABLE_ARRAY);

  FUNCTION replace_text_with_nested(
    p_schema_name     IN     VARCHAR2,
    p_table_name      IN     VARCHAR2,
    p_text_column     IN     VARCHAR2)
  RETURN DBMS_DATA_MINING_TRANSFORM.COLUMN_LIST;

  PROCEDURE create_table_from_query(query IN OUT NOCOPY LSTMT_REC_TYPE);

  PROCEDURE ls_append_expr(
    r_xform_lstmt     IN OUT NOCOPY LSTMT_REC_TYPE,
    p_expr_pat        IN VARCHAR2,
    p_col             IN VARCHAR2,
    p_col_pat         IN VARCHAR2 DEFAULT ':col',
    p_xform_settings  IN ODMR_TRANSFORM_SETTINGS DEFAULT NULL);

  PROCEDURE ls_prepend(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE,
    p_txt                          VARCHAR2);
  PROCEDURE ls_prepend(
    r_lstmt                        IN OUT NOCOPY LSTMT_REC_TYPE,
    p_txt                          LSTMT_REC_TYPE);

  FUNCTION upcase (
    p_in_name                      VARCHAR2)
  RETURN VARCHAR2;

  FUNCTION to_literal(
    p_val                          VARCHAR2)
  RETURN VARCHAR2;

  FUNCTION to_xpath_literal(
    p_val                          VARCHAR2)
  RETURN VARCHAR2;

  FUNCTION model_exists(p_model_schema IN VARCHAR2, p_model_name IN VARCHAR2, p_model_algorithm IN VARCHAR2, p_target IN VARCHAR2) RETURN BOOLEAN;

  FUNCTION db_table_exists(p_schema_name IN VARCHAR2, p_table_name IN VARCHAR2) RETURN BOOLEAN;

  FUNCTION db_table_empty_no_ucase(p_schema_name IN VARCHAR2, p_table_name IN VARCHAR2) RETURN BOOLEAN;
  
  FUNCTION db_table_empty(p_schema_name IN VARCHAR2, p_table_name IN VARCHAR2) RETURN BOOLEAN;

  FUNCTION db_table_empty(p_input_sql IN OUT NOCOPY LSTMT_REC_TYPE) RETURN BOOLEAN;

  FUNCTION db_table_granted_directly (p_schema_name IN VARCHAR2, p_table_name IN VARCHAR2) RETURN BOOLEAN;

  PROCEDURE request_lock (p_lock_name IN VARCHAR2, p_wait IN NUMBER DEFAULT SYS.DBMS_LOCK.MAXWAIT);

  FUNCTION request_lock (p_job_name IN VARCHAR2, p_chain_step IN VARCHAR2, p_wait IN NUMBER DEFAULT SYS.DBMS_LOCK.MAXWAIT) RETURN VARCHAR2;

  PROCEDURE release_lock(p_lock_name IN VARCHAR2);

  TYPE MSG_PARAMS is TABLE OF NVARCHAR2(4000);

  SUBTYPE STACK_TRACE is VARCHAR2(32767);

  PROCEDURE RAISE_ERR(
    p_error_code    IN NUMBER,
    p_params        IN MSG_PARAMS,
    p_stack_trace   IN STACK_TRACE DEFAULT '' );

  PROCEDURE DEBUG_MSG(
   p_message IN VARCHAR2 );

  PROCEDURE EVENT_LOG(p_job             IN VARCHAR2,
                      p_workflowId      IN NUMBER,
                      p_nodeId          IN VARCHAR2,
                      p_node_name       IN VARCHAR2 DEFAULT NULL,
                      p_subnode_id      IN VARCHAR2 DEFAULT NULL,
                      p_subnode_name    IN VARCHAR2 DEFAULT NULL,
                      p_message_type    IN VARCHAR2,
                      p_message_subtype IN VARCHAR2,
                      p_message_task    IN VARCHAR2,
                      p_duration        IN INTERVAL DAY TO SECOND DEFAULT NULL,
                      p_message_code    IN NUMBER DEFAULT NULL,
                      p_params          IN MSG_PARAMS DEFAULT NULL,
                      p_message_details IN VARCHAR2 DEFAULT NULL);

  DB_OBJECT_TABLE     CONSTANT VARCHAR2(30) := 'TABLE';
  DB_OBJECT_VIEW      CONSTANT VARCHAR2(30) := 'VIEW';
  DB_OBJECT_MODEL     CONSTANT VARCHAR2(30) := 'MODEL';
  DB_OBJECT_CHAIN     CONSTANT VARCHAR2(30) := 'CHAIN';
  DB_OBJECT_JOB       CONSTANT VARCHAR2(30) := 'JOB';
  DB_OBJECT_POLICY    CONSTANT VARCHAR2(30) := 'POLICY';
  DB_OBJECT_LEXER     CONSTANT VARCHAR2(30) := 'LEXER';
  DB_OBJECT_STOPLIST  CONSTANT VARCHAR2(30) := 'STOPLIST';

  TYPE DB_OBJECT IS RECORD (
    object_name VARCHAR2(30),
    object_type VARCHAR2(30), -- T - table/view, M - model, C - scheduler chain, J - scheduler job
    object_subtype VARCHAR2(4000), -- e.g. test metric, confusion matrix, lift, ROC, etc
    creation_date TIMESTAMP
    );
  TYPE DB_OBJECTS IS TABLE OF DB_OBJECT;

  PROCEDURE delete_db_objects(p_db_objects IN OUT NOCOPY DB_OBJECTS);

  FUNCTION is_numeric(p_value VARCHAR2) RETURN BOOLEAN;

  FUNCTION isCompatibleXmlSchemaVersion(p_version in varchar2) return BOOLEAN;

  -- return TRUE if repository XMLSchema >= p_version.  For example, it returns TRUE if repository XMLSchema is 11.2.1.1.1 and p_version is 11.2.0.1.9
  FUNCTION isCompatibleXMLSchema(p_version in varchar2) return BOOLEAN;

  -- return TRUE if database version >= p_version.  For example, it returns TRUE if database is 12.1.0.0.0 and p_version is 11.2.0.2.0
  FUNCTION isCompatibleDB(p_version in varchar2) return BOOLEAN;

  FUNCTION isCompatibleXMLSchemaAndDB(p_xmlversion in varchar2, p_dbversion in varchar2) return BOOLEAN;

  FUNCTION is_supported_target_datatype(p_datatype in varchar2, p_model_type in varchar2) return BOOLEAN;
  
  -- return true if datatype is supported for the model type
  FUNCTION is_supported_datatype(p_datatype in varchar2, p_model_type in varchar2) return BOOLEAN;

  -- garbage collect all user created objects only if all workflows are deleted
  PROCEDURE cleanup_user_repository;

  PROCEDURE add_table_to_workflow(p_object_name IN VARCHAR2, p_workflowId IN NUMBER);
  
END ODMR_INTERNAL_UTIL;
/
