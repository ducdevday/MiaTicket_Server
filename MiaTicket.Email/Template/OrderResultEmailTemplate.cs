﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Email.Template
{
    public class OrderResultEmailTemplate
    {
        private static readonly string _ticketTemplate = @"
  <tr>
    <td valign='top' class='esdev-mso-td' style='padding: 0; margin: 0'>
      <table align='left' cellpadding='0' cellspacing='0' class='es-left' role='none' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px; float: left;'>
        <tr>
          <td align='center' class='es-m-p0r' style='padding: 0; margin: 0; width: 70px'>
            <table cellspacing='0' width='100%' cellpadding='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px;'>
              <tr>
                <td align='left' style='padding: 0; margin: 0'>
                  <p style='margin: 0; mso-line-height-rule: exactly; font-family: arial, helvetica neue, helvetica, sans-serif; line-height: 21px; letter-spacing: 0; color: #333333; font-size: 14px;'>{{TicketNo}}</p>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
    <td style='padding: 0; margin: 0; width: 20px'></td>
    <td valign='top' class='esdev-mso-td' style='padding: 0; margin: 0'>
      <table cellpadding='0' cellspacing='0' align='left' class='es-left' role='none' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px; float: left;'>
        <tr>
          <td align='center' style='padding: 0; margin: 0; width: 265px'>
            <table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px;'>
              <tr>
                <td align='left' style='padding: 0; margin: 0'>
                  <p style='margin: 0; mso-line-height-rule: exactly; font-family: arial, helvetica neue, helvetica, sans-serif; line-height: 21px; letter-spacing: 0; color: #333333; font-size: 14px;'>{{TicketName}}</p>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
    <td style='padding: 0; margin: 0; width: 20px'></td>
    <td valign='top' class='esdev-mso-td' style='padding: 0; margin: 0'>
      <table cellpadding='0' cellspacing='0' align='left' class='es-left' role='none' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px; float: left;'>
        <tr>
          <td align='left' style='padding: 0; margin: 0; width: 80px'>
            <table cellpadding='0' cellspacing='0' width='100%' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px;'>
              <tr>
                <td align='center' style='padding: 0; margin: 0'>
                  <p style='margin: 0; mso-line-height-rule: exactly; font-family: arial, helvetica neue, helvetica, sans-serif; line-height: 21px; letter-spacing: 0; color: #333333; font-size: 14px;'>{{TicketQuantity}}</p>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
    <td style='padding: 0; margin: 0; width: 20px'></td>
    <td valign='top' class='esdev-mso-td' style='padding: 0; margin: 0'>
      <table cellpadding='0' cellspacing='0' align='right' class='es-right' role='none' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px; float: right;'>
        <tr>
          <td align='left' style='padding: 0; margin: 0; width: 85px'>
            <table cellspacing='0' width='100%' cellpadding='0' role='presentation' style='mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px;'>
              <tr>
                <td align='right' style='padding: 0; margin: 0'>
                  <p style='margin: 0; mso-line-height-rule: exactly; font-family: arial, helvetica neue, helvetica, sans-serif; line-height: 21px; letter-spacing: 0; color: #333333; font-size: 14px;'>{{TicketPrice}}</p>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
  </tr>";

        public static string _orderResultEmailTemplate = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" lang=\"und\">\r\n <head>\r\n  <meta charset=\"UTF-8\">\r\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n  <meta content=\"telephone=no\" name=\"format-detection\">\r\n  <title>New Template</title><!--[if (mso 16)]>\r\n    <style type=\"text/css\">\r\n    a {text-decoration: none;}\r\n    </style>\r\n    <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>\r\n<noscript>\r\n         <xml>\r\n           <o:OfficeDocumentSettings>\r\n           <o:AllowPNG></o:AllowPNG>\r\n           <o:PixelsPerInch>96</o:PixelsPerInch>\r\n           </o:OfficeDocumentSettings>\r\n         </xml>\r\n      </noscript>\r\n<![endif]-->\r\n  <style type=\"text/css\">\r\n.rollover:hover .rollover-first {\r\n  max-height:0px!important;\r\n  display:none!important;\r\n}\r\n.rollover:hover .rollover-second {\r\n  max-height:none!important;\r\n  display:block!important;\r\n}\r\n.rollover span {\r\n  font-size:0px;\r\n}\r\nu + .body img ~ div div {\r\n  display:none;\r\n}\r\n#outlook a {\r\n  padding:0;\r\n}\r\nspan.MsoHyperlink,\r\nspan.MsoHyperlinkFollowed {\r\n  color:inherit;\r\n  mso-style-priority:99;\r\n}\r\na.es-button {\r\n  mso-style-priority:100!important;\r\n  text-decoration:none!important;\r\n}\r\na[x-apple-data-detectors],\r\n#MessageViewBody a {\r\n  color:inherit!important;\r\n  text-decoration:none!important;\r\n  font-size:inherit!important;\r\n  font-family:inherit!important;\r\n  font-weight:inherit!important;\r\n  line-height:inherit!important;\r\n}\r\n.es-desk-hidden {\r\n  display:none;\r\n  float:left;\r\n  overflow:hidden;\r\n  width:0;\r\n  max-height:0;\r\n  line-height:0;\r\n  mso-hide:all;\r\n}\r\n@media only screen and (max-width:600px) {.es-m-p0r { padding-right:0px!important } .es-m-p20b { padding-bottom:20px!important } .es-p-default { } *[class=\"gmail-fix\"] { display:none!important } p, a { line-height:150%!important } h1, h1 a { line-height:120%!important } h2, h2 a { line-height:120%!important } h3, h3 a { line-height:120%!important } h4, h4 a { line-height:120%!important } h5, h5 a { line-height:120%!important } h6, h6 a { line-height:120%!important } .es-header-body p { } .es-content-body p { } .es-footer-body p { } .es-infoblock p { } h1 { font-size:36px!important; text-align:left } h2 { font-size:26px!important; text-align:left } h3 { font-size:20px!important; text-align:left } h4 { font-size:24px!important; text-align:left } h5 { font-size:20px!important; text-align:left } h6 { font-size:16px!important; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:36px!important } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:26px!important } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important } .es-header-body h4 a, .es-content-body h4 a, .es-footer-body h4 a { font-size:24px!important } .es-header-body h5 a, .es-content-body h5 a, .es-footer-body h5 a { font-size:20px!important } .es-header-body h6 a, .es-content-body h6 a, .es-footer-body h6 a { font-size:16px!important } .es-menu td a { font-size:12px!important } .es-header-body p, .es-header-body a { font-size:14px!important } .es-content-body p, .es-content-body a { font-size:14px!important } .es-footer-body p, .es-footer-body a { font-size:14px!important } .es-infoblock p, .es-infoblock a { font-size:12px!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3, .es-m-txt-c h4, .es-m-txt-c h5, .es-m-txt-c h6 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3, .es-m-txt-r h4, .es-m-txt-r h5, .es-m-txt-r h6 { text-align:right!important } .es-m-txt-j, .es-m-txt-j h1, .es-m-txt-j h2, .es-m-txt-j h3, .es-m-txt-j h4, .es-m-txt-j h5, .es-m-txt-j h6 { text-align:justify!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3, .es-m-txt-l h4, .es-m-txt-l h5, .es-m-txt-l h6 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-m-txt-r .rollover:hover .rollover-second, .es-m-txt-c .rollover:hover .rollover-second, .es-m-txt-l .rollover:hover .rollover-second { display:inline!important } .es-m-txt-r .rollover span, .es-m-txt-c .rollover span, .es-m-txt-l .rollover span { line-height:0!important; font-size:0!important; display:block } .es-spacer { display:inline-table } a.es-button, button.es-button { font-size:20px!important; padding:10px 20px 10px 20px!important; line-height:120%!important } a.es-button, button.es-button, .es-button-border { display:inline-block!important } .es-m-fw, .es-m-fw.es-fw, .es-m-fw .es-button { display:block!important } .es-m-il, .es-m-il .es-button, .es-social, .es-social td, .es-menu { display:inline-block!important } .es-adaptive table, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .adapt-img { width:100%!important; height:auto!important } .es-mobile-hidden, .es-hidden { display:none!important } .es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } .h-auto { height:auto!important } .es-text-3760 .es-text-mobile-size-20, .es-text-3760 .es-text-mobile-size-20 * { font-size:20px!important; line-height:150%!important } .es-text-6062 .es-text-mobile-size-48, .es-text-6062 .es-text-mobile-size-48 * { font-size:48px!important; line-height:150%!important } .es-text-7613 .es-text-mobile-size-16, .es-text-7613 .es-text-mobile-size-16 * { font-size:16px!important; line-height:150%!important } .es-text-7828 .es-text-mobile-size-18, .es-text-7828 .es-text-mobile-size-18 * { font-size:18px!important; line-height:150%!important } }\r\n@media screen and (max-width:384px) {.mail-message-content { width:414px!important } }\r\n</style>\r\n </head>\r\n <body class=\"body\" style=\"width:100%;height:100%;padding:0;Margin:0\">\r\n  <div dir=\"ltr\" class=\"es-wrapper-color\" lang=\"und\" style=\"background-color:#FAFAFA\"><!--[if gte mso 9]>\r\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\r\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\r\n\t\t\t</v:background>\r\n\t\t<![endif]-->\r\n   <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" class=\"es-wrapper\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FAFAFA\">\r\n     <tr>\r\n      <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n       <table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"es-content\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\r\n         <tr>\r\n          <td align=\"center\" class=\"es-info-area\" style=\"padding:0;Margin:0\">\r\n           <table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#00000000\" class=\"es-content-body\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\" role=\"none\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:20px;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-infoblock\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;letter-spacing:0;color:#CCCCCC;font-size:12px\"><a target=\"_blank\" href=\"\" style=\"mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\">​</a></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"es-content\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\r\n         <tr>\r\n          <td align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table bgcolor=\"#ffffff\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"es-content-body\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:0;Margin:0;padding-top:15px;padding-right:20px;padding-left:20px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-top:10px;padding-bottom:10px;font-size:0px\"><img src=\"https://fzolgd.stripocdn.email/content/guids/CABINET_9c045709b60d62e0436b7fe13c8e0da4e769b8e9a0a557a5ff47283ef0b9ad32/images/img_logo_primary.png\" alt=\"\" width=\"100\" style=\"display:block;font-size:14px;border:0;outline:none;text-decoration:none\"></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-text-3760\" style=\"padding:0;Margin:0;padding-bottom:10px\"><h1 class=\"es-m-txt-c es-text-mobile-size-20\" style=\"Margin:0;font-family:arial, 'helvetica neue', helvetica, sans-serif;mso-line-height-rule:exactly;letter-spacing:0;font-size:20px;font-style:normal;font-weight:bold;line-height:20px;color:#333333\"><strong>Order Successful</strong></h1></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"es-content\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\r\n         <tr>\r\n          <td align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table bgcolor=\"#ffffff\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"es-content-body\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:0;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:600px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-top:5px;padding-bottom:5px\"><h2 class=\"es-m-txt-c\" style=\"Margin:0;font-family:arial, 'helvetica neue', helvetica, sans-serif;mso-line-height-rule:exactly;letter-spacing:0;font-size:26px;font-style:normal;font-weight:bold;line-height:31.2px;color:#333333\">{{EventName}}</h2></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-text-7828\" style=\"padding:0;Margin:0;padding-top:5px;padding-bottom:5px\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Give this code to employee to check in</p></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-text-6062\" style=\"padding:0;Margin:0\"><h2 class=\"es-m-txt-c es-text-mobile-size-48\" style=\"Margin:0;font-family:arial, 'helvetica neue', helvetica, sans-serif;mso-line-height-rule:exactly;letter-spacing:0;font-size:48px;font-style:normal;font-weight:bold;line-height:72px;color:#f7a911\">{{QrCode}}</h2></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align class=\"es-text-7613\" style=\"padding:0;Margin:0;padding-top:5px;padding-bottom:5px;padding-left:10px\"><h2 class=\"es-m-txt-c es-text-mobile-size-16\" style=\"Margin:0;font-family:arial, 'helvetica neue', helvetica, sans-serif;mso-line-height-rule:exactly;letter-spacing:0;font-size:16px;font-style:normal;font-weight:bold;line-height:24px;color:#333333\">Ticket Information</h2></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n             <tr>\r\n              <td align=\"left\" class=\"esdev-adapt-off\" style=\"Margin:0;padding-right:20px;padding-left:20px;padding-top:10px;padding-bottom:10px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" class=\"esdev-mso-table\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:560px\">\r\n                 <tr>\r\n                  <td valign=\"top\" class=\"esdev-mso-td\" style=\"padding:0;Margin:0\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" align=\"left\" class=\"es-left\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left\">\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-m-p0r\" style=\"padding:0;Margin:0;width:70px\">\r\n                       <table cellspacing=\"0\" width=\"100%\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                         <tr>\r\n                          <td align=\"left\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\"><strong>No</strong></p></td>\r\n                         </tr>\r\n                       </table></td>\r\n                     </tr>\r\n                   </table></td>\r\n                  <td style=\"padding:0;Margin:0;width:20px\"></td>\r\n                  <td valign=\"top\" class=\"esdev-mso-td\" style=\"padding:0;Margin:0\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" align=\"left\" class=\"es-left\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;width:265px\">\r\n                       <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                         <tr>\r\n                          <td align=\"left\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\"><strong>Type</strong></p></td>\r\n                         </tr>\r\n                       </table></td>\r\n                     </tr>\r\n                   </table></td>\r\n                  <td style=\"padding:0;Margin:0;width:20px\"></td>\r\n                  <td valign=\"top\" class=\"esdev-mso-td\" style=\"padding:0;Margin:0\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" align=\"left\" class=\"es-left\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left\">\r\n                     <tr>\r\n                      <td align=\"left\" style=\"padding:0;Margin:0;width:80px\">\r\n                       <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                         <tr>\r\n                          <td align=\"center\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\"><strong>Quantity</strong></p></td>\r\n                         </tr>\r\n                       </table></td>\r\n                     </tr>\r\n                   </table></td>\r\n                  <td style=\"padding:0;Margin:0;width:20px\"></td>\r\n                  <td valign=\"top\" class=\"esdev-mso-td\" style=\"padding:0;Margin:0\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" align=\"right\" class=\"es-right\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right\">\r\n                     <tr>\r\n                      <td align=\"left\" style=\"padding:0;Margin:0;width:85px\">\r\n                       <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                         <tr>\r\n                          <td align=\"center\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\"><strong>Price</strong></p></td>\r\n                         </tr>\r\n                       </table></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n             <tr>\r\n              <td align=\"left\" class=\"esdev-adapt-off\" style=\"Margin:0;padding-right:20px;padding-left:20px;padding-top:10px;padding-bottom:10px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" class=\"esdev-mso-table\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:560px\">\r\n                 {{TicketItems}}\r\n               </table></td>\r\n             </tr>\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:0;Margin:0;padding-right:20px;padding-left:20px;padding-top:10px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" class=\"es-m-p0r\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;border-top:2px solid #efefef;border-bottom:2px solid #efefef\" role=\"presentation\">\r\n                     <tr>\r\n                      <td align=\"right\" style=\"padding:0;Margin:0;padding-top:10px;padding-bottom:20px\"><p class=\"es-m-txt-r\" style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Subtotal: {{SubTotalPrice}}<br>Discount: {{Discount}}<br>Total: {{TotalPrice}}</p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n             <tr>\r\n              <td align=\"left\" style=\"Margin:0;padding-right:20px;padding-left:20px;padding-bottom:10px;padding-top:20px\"><!--[if mso]><table style=\"width:560px\" cellpadding=\"0\" cellspacing=\"0\"><tr><td style=\"width:280px\" valign=\"top\"><![endif]-->\r\n               <table cellpadding=\"0\" cellspacing=\"0\" align=\"left\" class=\"es-left\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left\">\r\n                 <tr>\r\n                  <td align=\"center\" class=\"es-m-p0r es-m-p20b\" style=\"padding:0;Margin:0;width:280px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"left\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Customer: <strong>{{ReceiverName}}</strong></p><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Email: <strong>{{ReceiverEmail}}</strong></p><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Phone: <strong>{{ReceiverPhone}}</strong></p><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Payment method: <strong>{{PaymentType}}</strong></p><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Currency: <strong>{{Currency}}</strong></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table><!--[if mso]></td><td style=\"width:0px\"></td><td style=\"width:280px\" valign=\"top\"><![endif]-->\r\n               <table cellpadding=\"0\" cellspacing=\"0\" align=\"right\" class=\"es-right\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right\">\r\n                 <tr>\r\n                  <td align=\"center\" class=\"es-m-p0r\" style=\"padding:0;Margin:0;width:280px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"left\" style=\"padding:0;Margin:0\"><p class=\"es-m-txt-l\" style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">ShowTime: <strong>{{ShowTime}}</strong></p><p class=\"es-m-txt-l\" style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\">Address:</p><p class=\"es-m-txt-l\" style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#333333;font-size:14px\"><strong>{{Address}}</strong></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table><!--[if mso]></td></tr></table><![endif]--></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" class=\"es-content\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\r\n         <tr>\r\n          <td align=\"center\" class=\"es-info-area\" style=\"padding:0;Margin:0\">\r\n           <table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#00000000\" class=\"es-content-body\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\" role=\"none\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:20px;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-infoblock\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;letter-spacing:0;color:#CCCCCC;font-size:12px\"><a target=\"_blank\" href=\"\" style=\"mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\" style=\"mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\">Unsubscribe</a>.<a target=\"_blank\" href=\"\" style=\"mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\"></a></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table></td>\r\n     </tr>\r\n   </table>\r\n  </div>\r\n </body>\r\n</html>";

        /// <summary>
        ///  {{TicketNo}}, {{TicketName}}, {{TicketQuantity}}, {{TicketPrice}}
        /// </summary>
        /// <returns></returns>
        public static string GetTicketTemplate() => _ticketTemplate;
        /// <summary>
        /// {{EventName}}, {{QrCode}},{{SubTotalPrice}},{{Discount}},{{TotalPrice}}, {{ReceiverName}}, {{ReceiverEmail}}, {{ReceiverPhone}}, {{PaymentType}}, {{Currency}}, {{ShowTime}}, {{Address}}
        /// </summary>
        /// <returns></returns>
        public static string GetOrderResultEmailTemplate() => _orderResultEmailTemplate;
    }
}
