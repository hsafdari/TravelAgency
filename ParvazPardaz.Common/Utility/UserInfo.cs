using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.Common.Utility
{
    public static class UserInfo
    {
        public static string UserRequestInfo(HttpBrowserCapabilitiesBase request)
        {
            string Req = "ActiveXControls:" + request.ActiveXControls + "<br />" +
                "Adapters:" + request.Adapters + "<br />" +
                "AOL:" + request.AOL + "<br />" +
                "BackgroundSounds:" + request.BackgroundSounds + "<br />" +
                "Beta:" + request.Beta + "<br />" +
                "Browser:" + request.Browser + "<br />" +
                "Browsers:" + request.Browsers + "<br />" +
                "CanCombineFormsInDeck:" + request.CanCombineFormsInDeck + "<br />" +
                "CanInitiateVoiceCall:" + request.CanInitiateVoiceCall + "<br />" +
                "CanRenderAfterInputOrSelectElement:" + request.CanRenderAfterInputOrSelectElement + "<br />" +
                "CanRenderEmptySelects:" + request.CanRenderEmptySelects + "<br />" +
                "CanRenderInputAndSelectElementsTogether:" + request.CanRenderInputAndSelectElementsTogether + "<br />" +
                "CanRenderMixedSelects:" + request.CanRenderMixedSelects + "<br />" +
                "CanRenderOneventAndPrevElementsTogether:" + request.CanRenderOneventAndPrevElementsTogether + "<br />" +
                "CanRenderPostBackCards:" + request.CanRenderPostBackCards + "<br />" +
                "CanRenderSetvarZeroWithMultiSelectionList:" + request.CanRenderSetvarZeroWithMultiSelectionList + "<br />" +
                "CanSendMail:" + request.CanSendMail + "<br />" +
                "Capabilities:" + request.Capabilities + "<br />" +
                "CDF:" + request.CDF + "<br />" +
                "ClrVersion:" + request.ClrVersion + "<br />" +
                "Cookies:" + request.Cookies + "<br />" +
                "Crawler:" + request.Crawler + "<br />" +
                "DefaultSubmitButtonLimit:" + request.DefaultSubmitButtonLimit + "<br />" +
                "EcmaScriptVersion:" + request.EcmaScriptVersion + "<br />" +
                "Frames:" + request.Frames + "<br />" +
                "GatewayMajorVersion:" + request.GatewayMajorVersion + "<br />" +
                "GatewayMinorVersion:" + request.GatewayMinorVersion + "<br />" +
                "GatewayVersion:" + request.GatewayVersion + "<br />" +
                "HasBackButton:" + request.HasBackButton + "<br />" +
                "HidesRightAlignedMultiselectScrollbars:" + request.HidesRightAlignedMultiselectScrollbars + "<br />" +
                "HtmlTextWriter:" + request.HtmlTextWriter + "<br />" +
                "Id:" + request.Id + "<br />" +
                "InputType:" + request.InputType + "<br />" +
                "IsColor:" + request.IsColor + "<br />" +
                "IsMobileDevice:" + request.IsMobileDevice + "<br />" +
                "JavaApplets:" + request.JavaApplets + "<br />" +
                "JScriptVersion:" + request.JScriptVersion + "<br />" +
                "MajorVersion:" + request.MajorVersion + "<br />" +
                "MaximumHrefLength:" + request.MaximumHrefLength + "<br />" +
                "MaximumRenderedPageSize:" + request.MaximumRenderedPageSize + "<br />" +
                "MaximumSoftkeyLabelLength:" + request.MaximumSoftkeyLabelLength + "<br />" +
                "MinorVersion:" + request.MinorVersion + "<br />" +
                "MinorVersionString:" + request.MinorVersionString + "<br />" +
                "MobileDeviceManufacturer:" + request.MobileDeviceManufacturer + "<br />" +
                "MobileDeviceModel:" + request.MobileDeviceModel + "<br />" +
                "MSDomVersion:" + request.MSDomVersion + "<br />" +
                "NumberOfSoftkeys:" + request.NumberOfSoftkeys + "<br />" +
                "Platform:" + request.Platform + "<br />" +
                "PreferredImageMime:" + request.PreferredImageMime + "<br />" +
                "PreferredRenderingMime:" + request.PreferredRenderingMime + "<br />" +
                "PreferredRequestEncoding:" + request.PreferredRequestEncoding + "<br />" +
                "PreferredResponseEncoding:" + request.PreferredResponseEncoding + "<br />" +
                "RendersBreakBeforeWmlSelectAndInput:" + request.RendersBreakBeforeWmlSelectAndInput + "<br />" +
                "RendersBreaksAfterHtmlLists:" + request.RendersBreaksAfterHtmlLists + "<br />" +
                "RendersBreaksAfterWmlAnchor:" + request.RendersBreaksAfterWmlAnchor + "<br />" +
                "RendersBreaksAfterWmlInput:" + request.RendersBreaksAfterWmlInput + "<br />" +
                "RendersWmlDoAcceptsInline:" + request.RendersWmlDoAcceptsInline + "<br />" +
                "RendersWmlSelectsAsMenuCards:" + request.RendersWmlSelectsAsMenuCards + "<br />" +
                "RequiredMetaTagNameValue:" + request.RequiredMetaTagNameValue + "<br />" +
                "RequiresAttributeColonSubstitution:" + request.RequiresAttributeColonSubstitution + "<br />" +
                "RequiresContentTypeMetaTag:" + request.RequiresContentTypeMetaTag + "<br />" +
                "RequiresControlStateInSession:" + request.RequiresControlStateInSession + "<br />" +
                "RequiresDBCSCharacter:" + request.RequiresDBCSCharacter + "<br />" +
                "RequiresHtmlAdaptiveErrorReporting:" + request.RequiresHtmlAdaptiveErrorReporting + "<br />" +
                "RequiresLeadingPageBreak:" + request.RequiresLeadingPageBreak + "<br />" +
                "RequiresNoBreakInFormatting:" + request.RequiresNoBreakInFormatting + "<br />" +
                "RequiresOutputOptimization:" + request.RequiresOutputOptimization + "<br />" +
                "RequiresPhoneNumbersAsPlainText:" + request.RequiresPhoneNumbersAsPlainText + "<br />" +
                "RequiresSpecialViewStateEncoding:" + request.RequiresSpecialViewStateEncoding + "<br />" +
                "RequiresUniqueFilePathSuffix:" + request.RequiresUniqueFilePathSuffix + "<br />" +
                "RequiresUniqueHtmlCheckboxNames:" + request.RequiresUniqueHtmlCheckboxNames + "<br />" +
                "RequiresUniqueHtmlInputNames:" + request.RequiresUniqueHtmlInputNames + "<br />" +
                "RequiresUrlEncodedPostfieldValues:" + request.RequiresUrlEncodedPostfieldValues + "<br />" +
                "ScreenBitDepth:" + request.ScreenBitDepth + "<br />" +
                "ScreenCharactersHeight:" + request.ScreenCharactersHeight + "<br />" +
                "ScreenCharactersWidth:" + request.ScreenCharactersWidth + "<br />" +
                "ScreenPixelsHeight:" + request.ScreenPixelsHeight + "<br />" +
                "ScreenPixelsWidth:" + request.ScreenPixelsWidth + "<br />" +
                "SupportsAccesskeyAttribute:" + request.SupportsAccesskeyAttribute + "<br />" +
                "SupportsBodyColor:" + request.SupportsBodyColor + "<br />" +
                "SupportsBodyColor:" + request.SupportsBodyColor + "<br />" +
                "SupportsBold:" + request.SupportsBold + "<br />" +
                "SupportsCacheControlMetaTag:" + request.SupportsCacheControlMetaTag + "<br />" +
                "SupportsCallback:" + request.SupportsCallback + "<br />" +
                "SupportsCss:" + request.SupportsCss + "<br />" +
                "SupportsDivAlign:" + request.SupportsDivAlign + "<br />" +
                "SupportsDivNoWrap:" + request.SupportsDivNoWrap + "<br />" +
                "SupportsEmptyStringInCookieValue:" + request.SupportsEmptyStringInCookieValue + "<br />" +
                "SupportsFontColor:" + request.SupportsFontColor + "<br />" +
                "SupportsFontName:" + request.SupportsFontName + "<br />" +
                "SupportsFontSize:" + request.SupportsFontSize + "<br />" +
                "SupportsImageSubmit:" + request.SupportsImageSubmit + "<br />" +
                "SupportsIModeSymbols:" + request.SupportsIModeSymbols + "<br />" +
                "SupportsInputIStyle:" + request.SupportsInputIStyle + "<br />" +
                "SupportsInputMode:" + request.SupportsInputMode + "<br />" +
                "SupportsItalic:" + request.SupportsItalic + "<br />" +
                "SupportsJPhoneMultiMediaAttributes:" + request.SupportsJPhoneMultiMediaAttributes + "<br />" +
                "SupportsJPhoneSymbols:" + request.SupportsJPhoneSymbols + "<br />" +
                "SupportsQueryStringInFormAction:" + request.SupportsQueryStringInFormAction + "<br />" +
                "SupportsRedirectWithCookie:" + request.SupportsRedirectWithCookie + "<br />" +
                "SupportsSelectMultiple:" + request.SupportsSelectMultiple + "<br />" +
                "SupportsUncheck:" + request.SupportsUncheck + "<br />" +
                "SupportsXmlHttp:" + request.SupportsXmlHttp + "<br />" +
                "Tables:" + request.Tables + "<br />" +
                "TagWriter:" + request.TagWriter + "<br />" +
                "Type:" + request.Type + "<br />" +
                "UseOptimizedCacheKey:" + request.UseOptimizedCacheKey + "<br />" +
                "VBScript:" + request.VBScript + "<br />" +
                "Version:" + request.Version + "<br />" +
                "W3CDomVersion:" + request.W3CDomVersion + "<br />" +
                "Win16:" + request.Win16 + "<br />" +
                "Win32:" + request.Win32 + "<br />";

           return Req;
        }
    }
}
