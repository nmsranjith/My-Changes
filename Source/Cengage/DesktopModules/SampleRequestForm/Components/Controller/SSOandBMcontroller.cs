using System;
using System.Collections.Generic;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;

namespace DotNetNuke.Modules.SampleRequestForm.Components.Controller
{
    public class SSOandBMcontroller
    {
        /// <summary>
        /// Posting orders to Server using SSO Calls
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="srfItems"></param>
        /// <param name="type"></param>
        public int PostingDetailsToServer(SRFParameters sRFParameters, List<string> srfItems, ItemType type)
        {
            SSOAPIController controller = null;
            try
            {
                controller = new SSOAPIController();
                int ResponceCode = 0;
                switch (type)
                {
                    case ItemType.SSOREEDAMDIGITAL: // instructor details
                        sRFParameters.ISBNS = srfItems.ToArray();
                        
                        //sRFParameters.Token = string.Empty; // token will get by SSO instructor credentials
                        try
                        {
                            
                            var orderInstrcutorResults = controller.CreateIROrder(sRFParameters, out ResponceCode);

                            if (orderInstrcutorResults[0]["Value"]["Response"].ToString() == "true" 
                                && orderInstrcutorResults[0]["Value"]["ResultCode"].ToString() == "0")
                            {
                                DnnLog.Info("Orders Created Successfully of " + ItemType.SSOREEDAMDIGITAL.ToString() + "through CreateIROrder");
                            }
                            else
                            {                                
                                DnnLog.Error("Orders failured of " + ItemType.SSOREEDAMDIGITAL.ToString() + "through CreateIROrder");
                                return -1;
                            }
                            //DnnLog.Info(orderInstrcutorResults["Response"].ToString());
                        }
                        catch (Exception ex)
                        {
                            DnnLog.Info("Ranjith-->>SSOREEDAMDIGITAL-->> EXCEPTION-->" + ex.Message);
                            if (ResponceCode == 500)
                                return -5;
                            else if (ResponceCode == 400 || ex.Message.ToLower() == "the remote server returned an error: (400) bad request.")
                                return -4;
                            else
                                return -3;
                        }
                        return 0;
                        break;
                    case ItemType.SSOREEDAMCOURSESMART: // student details
                        foreach (string item in srfItems)
                        {
                            sRFParameters.CoreIsbn = item;
                            try
                            {                                
                                var consumeStudentResults = controller.GenerateConsumeIacForEisbn(sRFParameters, out ResponceCode); // student details with CourseSmart
                               
                                if (consumeStudentResults["Response"].ToString() == "true" && consumeStudentResults["ResultCode"].ToString() == "0")
                                {
                                    DnnLog.Info("Orders Created Successfully of " + ItemType.SSOREEDAMCOURSESMART.ToString() + "through GenerateConsumeIacForEisbn");
                                }
                                else
                                {                                   
                                    DnnLog.Error("Orders failured of " + ItemType.SSOREEDAMCOURSESMART.ToString() + "through GenerateConsumeIacForEisbn");
                                    return -11;
                                }                               
                               // DnnLog.Info(consumeStudentResults["Response"].ToString());
                            }
                            catch (Exception ex)
                            {
                                DnnLog.Info("Ranjith-->>SSOREEDAMCOURSESMART-->> EXCEPTION-->" + ex.Message);
                                if (ResponceCode == 500)
                                    return -15;
                                else if (ResponceCode == 400 || ex.Message.ToLower() == "the remote server returned an error: (400) bad request.")
                                    return -14;
                                else
                                    return -13;

                            }

                        }
                        return 0;
                        break;
                    case ItemType.PRINT:
                        //string contactID = string.Empty;// need to take from SSO webservice of instructor 
                        // print type isbns caaling mangallen web services
                        try
                        {
                            var printOrders = controller.CreateSample(sRFParameters.ContactID, srfItems.ToArray());
                            if (printOrders.Success)
                            {
                                DnnLog.Info("Created Samples succesfully of " + ItemType.PRINT.ToString());
                                DnnLog.Info("Message of Maggellean :" + printOrders.resultDescription + " - " + printOrders.Success);
                            }
                            else
                            {
                                DnnLog.Error("Failure in created Samples of " + ItemType.PRINT.ToString());
                                DnnLog.Info("Message of Maggellean :" + printOrders.resultDescription + " - " + printOrders.Success);
                                return -1;
                            }
                        }
                        catch (Exception ex)
                        {
                            DnnLog.Error("Exception occured in Print magallean :" + ex.Message);
                            return -3;
                        }
                        break;
                        return 0;
                    default:
                        //string contactIDfordigital = string.Empty;// need to take from SSO webservice 
                        //  type isbns caaling mangallen web services
                        try
                        {
                            var digitalOrders = controller.CreateSample(sRFParameters.ContactID, srfItems.ToArray());
                            if (digitalOrders.Success)
                            {
                                DnnLog.Info("Created Samples succesfully of digital items");
                                DnnLog.Info("Message of Maggellean :" + digitalOrders.resultDescription + " - " + digitalOrders.Success);
                            }
                            else
                            {
                                DnnLog.Error("Failure in created Samples of digital items");
                                DnnLog.Info("Message of Maggellean :" + digitalOrders.resultDescription + " - " + digitalOrders.Success);
                                return -1;
                            }
                        }
                        catch (Exception ex)
                        {
                            DnnLog.Error("Exception occured in digital magallean :" + ex.Message);
                            return -3;
                        }
                        break;
                        return 0;
                }

            }
            catch (Exception ex)
            {

                //throw ex;
            }
            return 0;

        }
    }


}