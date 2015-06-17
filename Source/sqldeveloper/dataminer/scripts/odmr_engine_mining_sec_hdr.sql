
  CREATE OR REPLACE PACKAGE "ODMRSYS"."ODMR_ENGINE_MINING_SEC" 
AS

  FUNCTION get_model_status(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER) RETURN VARCHAR2;

  PROCEDURE update_build_model_status(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_model_model_status(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_schema IN VARCHAR2, p_model_name IN VARCHAR2, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  --PROCEDURE update_refmodel_status(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_schema IN VARCHAR2, p_model_name IN VARCHAR2, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  FUNCTION get_result_status(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_result_type IN VARCHAR2, p_modelId IN NUMBER) RETURN VARCHAR2;

  PROCEDURE update_result_status(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_result_type IN VARCHAR2, p_modelId IN NUMBER, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_test_model_status(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_model_build(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER, p_creation_date IN TIMESTAMP, p_commit IN BOOLEAN);

  PROCEDURE insert_model_results(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_class_model_results(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_result_name IN VARCHAR2, p_result_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS, p_overwrite IN BOOLEAN, p_commit IN BOOLEAN);

  PROCEDURE update_regress_model_results(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_result_name IN VARCHAR2, p_result_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS, p_overwrite IN BOOLEAN, p_commit IN BOOLEAN);

  -- Persist the mining usage for the model
  PROCEDURE update_model_attr_usages(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER, 
                                     p_mod_input_attrs IN OUT NOCOPY ODMR_OBJECT_NAMES, p_mod_input_types IN OUT NOCOPY ODMR_OBJECT_NAMES,
                                     p_mod_mining_attrs IN OUT NOCOPY ODMR_OBJECT_NAMES, p_mod_mining_types IN OUT NOCOPY ODMR_OBJECT_NAMES,
                                     p_commit IN BOOLEAN);

  -- Persist the data usage for the model build
  --PROCEDURE update_model_data_prep_attrs(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_data_usage_attrs IN OUT NOCOPY ODMR_ENGINE_MINING.DU_OBJECTS, p_commit IN BOOLEAN);

  PROCEDURE get_case_columns(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_caseid_columns IN OUT NOCOPY ODMR_OBJECT_NAMES, p_caseid_cols_type IN OUT NOCOPY ODMR_OBJECT_NAMES);

  --FUNCTION get_composite_case_column(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_caseid_columns IN OUT NOCOPY VARCHAR2) RETURN NUMBER;
  FUNCTION get_composite_case_column(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_case_columns IN OUT NOCOPY VARCHAR2, p_case_columns_ex IN OUT NOCOPY VARCHAR2) RETURN NUMBER;

  PROCEDURE get_build_node_info(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_build_type OUT VARCHAR2, p_build_name OUT VARCHAR2, p_model_type OUT VARCHAR2, p_model_name OUT VARCHAR2);

  PROCEDURE get_model_node_info(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_mining_function OUT VARCHAR2, p_mining_target OUT VARCHAR2, p_model_schemas IN OUT NOCOPY ODMR_OBJECT_NAMES, p_model_names IN OUT NOCOPY ODMR_OBJECT_NAMES, p_model_algorithms IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE get_test_node_info(p_workflowId IN NUMBER, p_run_mode IN VARCHAR2, p_nodeId IN VARCHAR2, p_archive OUT VARCHAR2, p_mining_function OUT VARCHAR2, p_mining_target OUT VARCHAR2, p_model_schemas IN OUT NOCOPY ODMR_OBJECT_NAMES, p_model_names IN OUT NOCOPY ODMR_OBJECT_NAMES, p_modelIds IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE get_target_column(p_workflowId NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_target_column OUT VARCHAR2, p_target_datatype OUT VARCHAR2);

  PROCEDURE get_create_balance_weight(p_workflowId IN NUMBER, p_run_mode IN VARCHAR2,
                                  p_nodeId IN VARCHAR2,
                                  p_isNB OUT NUMBER, p_isDT OUT NUMBER, p_isSVMC OUT NUMBER, p_isGLMC OUT NUMBER);

  PROCEDURE get_create_user_weight(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId in NUMBER,
                                   p_target_values IN OUT NOCOPY ODMR_OBJECT_VALUES, p_target_weights IN OUT NOCOPY ODMR_ENGINE_MINING.WEIGHTS);

  PROCEDURE get_create_prior(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId in NUMBER,
                             p_target_values IN OUT NOCOPY ODMR_OBJECT_VALUES, p_target_probs IN OUT NOCOPY ODMR_ENGINE_MINING.WEIGHTS);

  PROCEDURE get_model_cost_matrix(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId in NUMBER,
                               p_actuals IN OUT NOCOPY ODMR_OBJECT_VALUES, p_predicts IN OUT NOCOPY ODMR_OBJECT_VALUES, p_costs IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE get_build_settings(p_workflowId IN NUMBER, p_model_type IN VARCHAR2, p_nodeId IN NUMBER, p_modelId IN NUMBER,
           p_algorithm_setting_names IN OUT NOCOPY ODMR_OBJECT_NAMES, p_algorithm_setting_values IN OUT NOCOPY ODMR_OBJECT_VALUES);

  PROCEDURE get_assoc_model_info(p_workflowId NUMBER, p_nodeId IN VARCHAR2, p_item_id OUT VARCHAR2, p_item_type OUT VARCHAR2, p_item_value OUT VARCHAR2, p_item_value_type OUT VARCHAR2, p_max_value_cnt OUT NUMBER);
  
  FUNCTION use_auto_data_prep(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2) RETURN BOOLEAN;

  FUNCTION is_manual_data_input(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER) RETURN BOOLEAN;

  FUNCTION is_manual_mining_input(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER) RETURN BOOLEAN;

  PROCEDURE get_build_mining_attributes(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2,
                                        p_attributes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrDataTypes IN OUT NOCOPY ODMR_OBJECT_NAMES, 
                                        p_attrMiningTypes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrInputTypes IN OUT NOCOPY ODMR_OBJECT_NAMES);

  --PROCEDURE get_auto_mining_attributes(p_workflowId IN NUMBER, p_run_mode IN VARCHAR2, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2,
  --                                     p_attributes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrDataTypes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrMiningTypes IN OUT NOCOPY ODMR_OBJECT_NAMES);

  --PROCEDURE get_create_auto_data_usage(p_workflowId IN NUMBER, p_run_mode IN VARCHAR2, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2,
  --                                      p_attributes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrDataTypes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrMiningTypes IN OUT NOCOPY ODMR_OBJECT_NAMES,
  --                                      p_usage_models IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE get_create_manual_data_usage(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER,
                                        p_attributes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrDataTypes IN OUT NOCOPY ODMR_OBJECT_NAMES,
                                        p_attrMiningTypes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrInputTypes IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE get_auto_data_prep_attrs(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER,
                                     p_attributes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrInputTypes IN OUT NOCOPY ODMR_OBJECT_NAMES,
                                     p_attrAutoPreps IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrDataTypes IN OUT NOCOPY ODMR_OBJECT_NAMES, p_attrMiningTypes IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE get_generate_weight_option(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER,
                                       p_isBalanced OUT NUMBER, p_isNatural OUT NUMBER, p_isCustom OUT NUMBER);

  PROCEDURE get_model_tune_option(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN NUMBER,
                                     p_isNone OUT NUMBER, p_isCost OUT NUMBER, p_isBenefit OUT NUMBER, p_isCustom OUT NUMBER);

  PROCEDURE get_classification_result(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_result_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS);

  PROCEDURE get_classification_results(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_results IN OUT NOCOPY ODMR_ENGINE_MINING.RS_OBJECTS);

  PROCEDURE get_regression_result(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_result_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS);

  PROCEDURE get_regression_results(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN NUMBER, p_results IN OUT NOCOPY ODMR_ENGINE_MINING.RS_OBJECTS);

  PROCEDURE get_build_data_source(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_buildSourceId OUT NUMBER);

  FUNCTION get_build_first_model(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2) RETURN VARCHAR2;

  PROCEDURE get_build_test_data_option(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_useBuildData OUT NUMBER, p_useTestData OUT NUMBER, p_testSourceId OUT VARCHAR2, p_useSplitData OUT NUMBER, p_testPercent OUT NUMBER);

  PROCEDURE get_build_test_target_option(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_isTopN OUT NUMBER, p_TopNTargetValue OUT NUMBER, p_isBottomN OUT NUMBER, p_BottomNTargetValue OUT NUMBER, p_isCustom OUT NUMBER, p_target_values IN OUT NOCOPY ODMR_OBJECT_VALUES);

  PROCEDURE update_mining_attribute_status(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_attribute IN VARCHAR2, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_case_attribute_status(p_workflowId IN NUMBER, p_node_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_attribute IN VARCHAR2, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_target_attribute_status(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_nodeId IN VARCHAR2, p_attribute IN VARCHAR2, p_status IN VARCHAR2, p_commit IN BOOLEAN);

  PROCEDURE update_item_value_status(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_itemValue IN VARCHAR2, p_status IN VARCHAR2, p_commit IN BOOLEAN);
/*
  PROCEDURE update_balance_weights(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN VARCHAR2, 
                                   v_actual_values IN OUT NOCOPY ODMR_OBJECT_VALUES, v_predicted_values IN OUT NOCOPY ODMR_OBJECT_VALUES, v_costs IN OUT NOCOPY ODMR_OBJECT_IDS);

  PROCEDURE update_balance_weights(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN VARCHAR2, 
                                   v_target_values IN OUT NOCOPY ODMR_OBJECT_VALUES, v_weights IN OUT NOCOPY ODMR_OBJECT_IDS);
*/
  FUNCTION get_balance_weights(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN VARCHAR2) RETURN VARCHAR2;

  FUNCTION update_balance_weights(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_type IN VARCHAR2, p_modelId IN VARCHAR2, p_weight_table IN VARCHAR2, p_commit IN BOOLEAN) RETURN VARCHAR2;

  PROCEDURE get_apply_refmodels(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_model_schemas IN OUT NOCOPY ODMR_OBJECT_NAMES, p_model_names IN OUT NOCOPY ODMR_OBJECT_NAMES);

  PROCEDURE delete_test(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_modelId IN VARCHAR2, p_db_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS);

  PROCEDURE delete_model(p_workflowId IN NUMBER, p_build_type IN VARCHAR2, p_modelType IN VARCHAR2, p_modelId IN VARCHAR2, p_db_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS);

  PROCEDURE delete(p_workflowId IN NUMBER, p_nodeId IN VARCHAR2, p_db_objects IN OUT NOCOPY ODMR_INTERNAL_UTIL.DB_OBJECTS);

END;
/
