using System;

class RequestValidatorDisabled : System.Web.Util.RequestValidator 
{ 
    protected override bool IsValidRequestString(System.Web.HttpContext context, string value, System.Web.Util.RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex) 
    { 
        
        validationFailureIndex = -1; 
        
        return true; 
    } 
}
