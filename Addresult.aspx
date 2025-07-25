<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="Addresult.aspx.cs" Inherits="Addresult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
        input[type="checkbox"] {
    width:30px;
    height:26px
}
        #MainContent_GVAAresultEntrySub tbody tr th:nth-child(4) {
            width:66px
        }
             #MainContent_GVAAresultEntrySub tbody tr th:nth-child(3) {
            width:261px
        }
        #MainContent_GVAAresultEntrySub tbody tr td span {
        font-size:15px
        }
          #MainContent_GVAAresultEntrySub tbody tr td {
       padding:0px 3px
        }
       
    </style>
<asp:scriptmanager  id="scr" runat="server">
    </asp:scriptmanager>
                                  
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Add Result</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Add Result</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                           <div class="row mb-2">
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txtregno"  placeholder="Enter Reg No" TabIndex="5" runat="server"  class="form-control pull-right" AutoPostBack="True" OnTextChanged="txtregno_TextChanged"></asp:TextBox>
                                  <div style="display: none; overflow: scroll; width: 1000px; height: 100px; text-align: right"
                                                                                id="Div1">
                                                                            </div>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" BehaviorID="HHHHH"
                                                                                CompletionListElementID="Div1" ServiceMethod="GetPatientInfo_regno" TargetControlID="txtregno"
                                                                                 CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                                MinimumPrefixLength="1">
                                                                            </cc1:AutoCompleteExtender>
                                          </div>
                                </div>
                                  <div class="col-sm-1">
                                    <div class="form-group">
<asp:Button ID="btnaction" runat="server" class="btn btn-primary" Text="Action" OnClick="btnaction_Click"></asp:Button>
                                        </div>
                                     </div>
                               <div class="col-sm-7">
                                   <asp:Button ID="Button6" runat="server" class="btn btn-warning" 
                                        Text="Auto Calc" onclick="btncalculate_Click"    />
                                    
                                   <asp:Button ID="Button7" runat="server" class="btn btn-primary"  Text="Save & Close" ToolTip="Save & Close" OnClick="btnsaveClose_Click"  />
                                        <asp:Button ID="Button8" runat="server" class="btn btn-success" Text="Save"  OnClick="btnSaveAll_Click"
                            ValidationGroup="frmValid" OnClientClick="show()"  />
                                    <asp:Button ID="Button9" runat="server" class="btn btn-warning"  Text="Back" ToolTip="Back" OnClick="btnbackbutton_Click" />
                                    <asp:Button ID="Button10" runat="server" class="btn btn-success" OnClick="btnreport_Click" ValidationGroup="frmValid"
                            OnClientClick="show()" Text="Save and Print" ToolTip="Save and Print"
                             />
                                   <asp:Button ID="btnprintprieve1" runat="server" class="btn btn-success"  ValidationGroup="frmValid"
                            OnClientClick="show()" Text="preview" ToolTip="Save and Print preview" OnClick="btnprintprieve_Click" />
                               </div>
                                 <div id="Div2" class="col-sm-6" runat="server" >
                                    <div class="form-group">
                                       <asp:label id="Label3" runat="server" Font-Bold="true" ForeColor="Green" text="" ></asp:label>
                                <asp:Label ID="Label44" runat="server" Font-Bold="True" ForeColor="Red" Width="455px"></asp:Label>
        <asp:Label ID="lblSelectDocError" runat="server" SkinID="errmsg"></asp:Label>
                                        
                                       
                                    </div>
                                </div>
                                 <div id="Div3" class="col-sm-3" runat="server" visible="false" >
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPatientName"  placeholder="Enter Patient Name" TabIndex="5" runat="server"  class="form-control pull-right" AutoPostBack="True" OnTextChanged="txtPatientName_TextChanged"></asp:TextBox>
                                     <div style="display: none; overflow: scroll; width: 1000px; height: 100px; text-align: right"
                                                                                id="Div4">
                                                                            </div>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="fff"
                                                                                CompletionListElementID="Div2" ServiceMethod="GetPatientInfo" TargetControlID="txtPatientName"
                                                                                 CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                                MinimumPrefixLength="1">
                                                                            </cc1:AutoCompleteExtender>
                                        
                                        <asp:TextBox ID="txtMobileNo"  placeholder="Enter Mobile Number" TabIndex="5" runat="server"  class="form-control pull-right" AutoPostBack="True" OnTextChanged="txtMobileNo_TextChanged"></asp:TextBox>
                                     <div style="display: none; overflow: scroll; width: 1000px; height: 100px; text-align: right"
                                                                                id="Div5">
                                                                            </div>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" BehaviorID="tt"
                                                                                CompletionListElementID="Div3" ServiceMethod="GetPatientInfo_Mobile"
                                                                                 CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtMobileNo"
                                                                                MinimumPrefixLength="1">
                                                                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                
                                 </div>
                            <div class="row mb-2">
                             <div class="col-sm-3">
                             <span><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo" align="left" Font-Bold="True" Width="70px"></asp:Label></strong></span>
                             </div>
                             
                                <div class="col-sm-3"><span><strong>Name :  <asp:Label ID="lblName" runat="server" Text="Name" align="left"  Font-Bold="True" Width="186px"></asp:Label></strong></span></div>
                               
                                <div class="col-sm-3"><span><strong>DOB / Age :  <asp:Label ID="lblage" ForeColor="Red" runat="server" Text="Age" align="left"  Font-Bold="True" Width="151px"></asp:Label></strong></span></div>
                               
                                <div class="col-sm-3"><span><strong>Gender :  <asp:Label ID="lblSex" align="left" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                              
                                       
                                </div>
                            <div class="row mb-2">
                                <div class="col-sm-3"><span><strong>Mobile : <asp:Label ID="LblMobileno" align="left" runat="server" Width="96px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                            
                                <div class="col-sm-3"><span><strong>Center :  <asp:Label ID="Lblcenter" align="left" runat="server" Width="175px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                              
                                <div class="col-sm-3"><span><strong>Date : <asp:Label ID="lbldate" runat="server"  align="left" Font-Bold="True" Width="142px"></asp:Label></strong></span></div>
                              
                                <div class="col-sm-3"><span><strong>Ref Dr. :<asp:Label ID="LblRefDoc" align="left" Font-Bold="True" runat="server"  Width="180px"></asp:Label></strong></span>

                                </div>
                                    </div>
                           
                               <div class="row mb-2" runat="server" visible="false" id="D2">
                               <div class="col-sm-2" >
                             <span><strong>Weight # : <asp:Label ID="lblweight" runat="server" align="left" Text=""  Font-Bold="True" Width="70px"></asp:Label></strong></span>
                             </div>
                                <div class="col-sm-1" runat="server" visible="false" id="D3"></div>
                                <div class="col-sm-2" runat="server" visible="false" id="D4"><span><strong>Height :  <asp:Label ID="lblheight" align="left"  runat="server" Text=""  Font-Bold="True" Width="186px"></asp:Label></strong></span></div>
                                <div class="col-sm-1" runat="server" visible="false" id="D5"></div>
                                <div class="col-sm-6" runat="server" visible="false" id="D6"><span><strong>Dieses :  <asp:Label ID="lbldieses" align="left" runat="server" Text=""  Font-Bold="True" Width="460px"></asp:Label></strong></span></div>
                                
                               </div>
                                   <div class="row mb-2" runat="server" visible="false" id="D9">
                               
                               <div class="col-sm-2" >
                             <span><strong>Last Period # : <asp:Label ID="lbllastperiod" align="left" runat="server" Text=""  Font-Bold="True" Width="70px"></asp:Label></strong></span>
                             </div>
                                <div class="col-sm-1" runat="server" visible="false" id="D10"></div>
                                <div class="col-sm-2" runat="server" visible="false" id="D11"><span><strong>FS Time :  <asp:Label align="left" ID="lblfstime" runat="server"  Text=""  Font-Bold="True" Width="186px"></asp:Label></strong></span></div>
                                <div class="col-sm-1" runat="server" visible="false" id="D12"></div>
                                <div class="col-sm-6" runat="server" visible="false" id="D13"><span><strong>Therapy :  <asp:Label align="left" ID="lbltherpay" runat="server" Text=""  Font-Bold="True" Width="450px"></asp:Label></strong></span></div>
                               
                                       </div>
                                        <div class="row">
                              
                               <div class="col-sm-12" runat="server" visible="false" id="D16">
                             <span><strong>Symptoms # : <asp:Label ID="lblsymptoms" align="left" runat="server" Text=""  Font-Bold="True" Width="970px"></asp:Label></strong></span>
                             <asp:Label ID="lblPatusername" align="left" runat="server" Text="" Visible="false"  Font-Bold="True" Width="970px"></asp:Label>
                                    </div>
                                </div>
                             <div class="col-lg-12 mt-3">
                            <strong>Clinical History:</strong>   <asp:Label ID="Label4" runat="server" ForeColor="Red" Text=" "  Font-Bold="True"></asp:Label>
                              </div>
                            <div class="row">
                              
                               <div class="col-sm-12" runat="server" visible="false" id="D15">
                                  &nbsp; </br>
                                   </div>
                                </div>
                            
                                 
                                       <div class="table-responsive" style="width:100%">
                                     <!-- <div class="rounded_corners" style="width:100%"> -->
                                      <table class="table table-responsive table-sm"  cellpadding="2" cellspacing="0" width="100%" >
                <tr>
                    <td align="left" valign="top">  
                   
                   <asp:UpdatePanel runat="server" id="upd">
                      <contenttemplate>
                      
 <div>
      <div>
                    <asp:GridView ID="GVAAresultEntrySub" class="table table-responsive table-sm  table-bordered" runat="server"  Width="100%"  
                                    AutoGenerateColumns="False" DataKeyNames="MainTestName" onrowdatabound="GVAAresultEntrySub_RowDataBound" 
                                    onrowcommand="GVAAresultEntrySub_RowCommand" 
                              onrowcreated="GVAAresultEntrySub_RowCreated" >
                         <Columns>
                    <asp:TemplateField HeaderText="Dept Name">
                            <ItemTemplate>
                                <asp:Label ID="Lblmaintestname"  Width="150px"   runat="server" Font-Bold="true" 
                                    Text='<%#Eval("MainTestName") %>'  ></asp:Label>
                                     <asp:Label ID="Lblmaintestname1" runat="server" Visible="false" Font-Bold="true" 
                                    Text='<%#Eval("MainTestName") %>'  ></asp:Label>
                                    
                                      <asp:Label ID="lbl_MT_Code" Visible="false"  runat="server" Text='<%#Eval("MTCode") %>'></asp:Label>
                                       <asp:Label ID="lbltexdes" Visible="false" runat="server" Text='<%#Eval("TextDesc") %>'></asp:Label>
                                         <asp:Label ID="lblsubdeptid1" Visible="false" runat="server" Text='<%#Eval("subdeptid") %>'></asp:Label>
                                          <asp:Label ID="lblSDCode" runat="server" Visible="false" Text='<%#Eval("SDCode") %>'></asp:Label> <!--lblHcode -->
                           <asp:Label ID="lblsubdept" runat="server" Visible="false" Text='<%#Eval("subdeptName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Test Name" ControlStyle-Width="150px" >
                            <ItemTemplate>
                               
                                 <asp:Label ID="lblTtestname" Font-Names="verdana"   Text='<%#Eval("testname") %>' Width="150px"  runat="server"></asp:Label>
                            </ItemTemplate>
                            
                                 <ControlStyle Width="150px" />
                            
                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Result" >
                            <ItemTemplate  >
                                <asp:TextBox ID="txt_EnterResult" class="form-control"  BorderColor="#0099cc"   AutoPostBack="false" runat="server" 
                                    Text='<%#Eval("ResultTemplate") %>' Width="220px"
                                    ontextchanged="txt_EnterResult_TextChanged" ></asp:TextBox>
                               
                                      <asp:Label ID="lblTestCode" runat="server" Width="270px"  Visible="false" Text='<%#Eval("STCODE") %>'></asp:Label>
                         <asp:ImageButton ID="btndescresult" runat="server" Width="40px" ToolTip="Descriptive Report"  CommandArguement='<%#Eval("MTCode")%>'  ImageUrl="~/Images0/edit.gif" 
                                    onclick="btndescresult_Click"></asp:ImageButton>
                                    <asp:Label ID="lblNormalRange" runat="server" Width="270px"   Visible="false" Text='<%#Eval("normalRange") %>' ></asp:Label>
                                    <asp:TextBox ID="txtrange" runat="server" Width="270px"  Text="" Visible="false"></asp:TextBox>
                                     <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="3"  Width="270px"  Text='' Visible="false"
                                                            runat="server" ></asp:TextBox>
                                  <asp:Label ID="lblMTCodesub" Visible="false"  runat="server" Text='<%#Eval("MTCode") %>'></asp:Label>

                                   <asp:ImageButton ID="Imghelp" runat="server" Width="20px" Visible="false" TabIndex="1001" style="margin-bottom: -7px;height:25px"  onclick="Imghelp_Click" ToolTip="Short Form"  CommandArguement='<%#Eval("MTCode")%>'  ImageUrl="~/Images0/Calendar_scheduleHS.png"   ></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Auth"   ControlStyle-Width="66px" >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkautho" runat="server"  TabIndex="9991" BorderColor="Red" ForeColor="Red" 
                                    Width="30px" AutoPostBack="True" oncheckedchanged="chkautho_CheckedChanged" ></asp:CheckBox>
                                 <asp:Label ID="lblStatus" Text='<%#Eval("Patauthicante") %>' Width="10px" Visible="false" runat="server"></asp:Label>
                            
                            <asp:ImageButton ID="ImgdescN" runat="server" style="margin-left:18px" Width="20px" TabIndex="9992"  ToolTip="Descriptive / Text"  CommandArguement='<%#Eval("MTCode")%>'  ImageUrl="~/Images0/Calendar_scheduleHS.png"  onclick="ImgdescN_Click" ></asp:ImageButton>
                           <asp:ImageButton ID="Imgrerun" runat="server" OnClientClick="return ValidateDelete();"  style="margin-left:6px" Width="20px"  TabIndex="9993" ToolTip="ReRun"  CommandArguement='<%#Eval("MTCode")%>'  ImageUrl="~/images/ReRun.png"  onclick="Imgrerun_Click" ></asp:ImageButton>
                          
                         
                            </ItemTemplate>
                            
                                  <ControlStyle Width="20px" />
                            
                        </asp:TemplateField>

                              <asp:TemplateField HeaderText="QC">
                            <ItemTemplate >  
                                  <asp:CheckBox ID="chkQc" runat="server"   TabIndex="9992" BorderColor="Red" ForeColor="Red" 
                                    Width="30px" AutoPostBack="True" oncheckedchanged="chkQc_CheckedChanged" ></asp:CheckBox>
                                <asp:Label ID="lblQcCheck" Text='<%#Eval("QcCheck") %>' Width="10px" Visible="false" runat="server"></asp:Label>
                                </ItemTemplate>
                                  </asp:TemplateField>

                               <asp:TemplateField HeaderText="ShortForm ">
                            <ItemTemplate>                               
                                <asp:DropDownList ID="DdlShortForm" style="padding-bottom: 8px" TabIndex="9993" class="form-control form-select"  Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlShortForm_SelectedIndexChanged"></asp:DropDownList>
                                 
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                              <asp:TemplateField HeaderText="Previous Result" >
                            <ItemTemplate  >
                                 <asp:Label ID="lblPrevResultDate" runat="server"   ForeColor="Red"  Width="100px"   Text='<%#Eval("PrevResultDate") %>' ></asp:Label>
                                <asp:TextBox ID="txt_EnterPrevResult" runat="server" class="form-control"  Width="90px" Enabled="false"  Text='<%#Eval("PReviousResult") %>' ></asp:TextBox>
                                </ItemTemplate>
                                  </asp:TemplateField>
                             <asp:TemplateField HeaderText="Normal Range" >
                            <ItemTemplate  >
                             <asp:Label ID="lblNormalRange1" runat="server" Width="80px"   Text='<%#Eval("normalRange") %>' ></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Panic Range" >
                            <ItemTemplate  >
                             <asp:Label ID="lblPanicRange" runat="server" Font-Bold="true" BackColor="Red" ForeColor="Wheat" Width="80px"   Text='<%#Eval("PanicRange") %>' ></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                                   <asp:BoundField DataField="unit" HeaderText="Unit" >
                                   <ItemStyle Width="100px" />
                            </asp:BoundField>
                            
                         <asp:TemplateField HeaderText="Doctor ">
                            <ItemTemplate>                               
                                <asp:DropDownList ID="ddldoctor" TabIndex="9994" class="form-control form-select"  Width="100px" runat="server"></asp:DropDownList>
                                 <asp:Label ID="Lblsubdeptid" runat="server"  Visible="false" Text='<%#Eval("subdeptid") %>' Width="100px"></asp:Label>
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="" >
                            <ItemTemplate  >
                              <asp:ImageButton ID="btnreport" runat="server" Width="28px" style="padding-left:6px" 
                                    CommandArguement='<%#Eval("MTCode")%>' TabIndex="9995"  ImageUrl="~/Images0/print.png" onclick="btnreport_Click1" 
                                    ></asp:ImageButton>
                               
                                 <asp:Button  id="NORep" runat="server" TabIndex="9889" Visible="false" Text=" Upload File" class="btn btn-primary" OnClick="NORep_Click" >
                                                               
                                                            </asp:Button>
                                  <asp:TextBox ID="txtSampDate" runat="server" TabIndex="9997" class="form-control"  Width="150px"  Text="" ></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                              
                               <asp:TemplateField HeaderText="Remark ">
                            <ItemTemplate >  
                                
                            <asp:TextBox ID="txtTestRemark" runat="server" TabIndex="9996"  Width="100px" TextMode="MultiLine" Text="" ></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                         <asp:TemplateField HeaderText="Technician " Visible="false">
                            <ItemTemplate >                               
                                <asp:DropDownList ID="ddlTechnician" class="form-control form-select"  Width="100px" runat="server"></asp:DropDownList>
                                 <asp:Label ID="lblDocName" runat="server"  Text='<%#Eval("AunticateSignatureId") %>' Visible="false" ></asp:Label>
                                    <asp:Label ID="lblsign" runat="server"   Text='<%#Eval("AunticateSignatureId") %>' Visible="false"></asp:Label>
                                    
                                 <asp:Label ID="LblErrMsg" ForeColor="Red"   Text='' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                                </Columns>
                                </asp:GridView>
          </div>
                          </div>  
                        
                        </contenttemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
               
            </table>
                                  <!--  </div>-->
                        
                               
                            </div>
                          
                                
                            </div>
                       
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   <asp:Button ID="btncalculate" runat="server" class="btn btn-warning" 
                                        Text="Auto Calc" onclick="btncalculate_Click"    />  
                                  <asp:Button ID="btnSaveAll" runat="server" class="btn btn-success" Text="Save"  OnClick="btnSaveAll_Click"
                            ValidationGroup="frmValid" OnClientClick="show()"  />
                                    <asp:Button ID="btnreport" runat="server" class="btn btn-success" OnClick="btnreport_Click" ValidationGroup="frmValid"
                            OnClientClick="show()" Text="Save and Print" ToolTip="Save and Print"
                             />
                                     <asp:Button ID="btnbackbutton" runat="server" class="btn btn-warning"  Text="Back" ToolTip="Back" OnClick="btnbackbutton_Click" />
                                   <asp:Button ID="btnsaveClose" runat="server" class="btn btn-primary"  Text="Save & Close" ToolTip="Save & Close" OnClick="btnsaveClose_Click"  />
                                     <asp:Button ID="btnprintprieve" runat="server" class="btn btn-success"  ValidationGroup="frmValid"
                            OnClientClick="show()" Text="preview" ToolTip="Save and Print preview" OnClick="btnprintprieve_Click"
                             />
                                </div>
                            </div>
                        </div>
                    </div>
                      <div class="modal" id="largeShoes" tabindex="-1" role="dialog" aria-labelledby="modalLabelLarge" aria-hidden="true">
        <div class="modal-dialog modal-lg" style="width: 70%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="modalLabelLarge">No Report</h4>
                </div>

                <div class="modal-body">
                    <div class="list-group">
                        <asp:CheckBoxList ID="Chkmaintestshort" type="checkbox" CssClass="form-check list-group-item" name="CheckBoxInputName" runat="server" RepeatColumns="7" Width="100%" AutoPostBack="false" >
                        </asp:CheckBoxList>
                        <table>
                            <tr>
                                <td>
                                    
                                    <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="No Report" ></asp:Label>
                                </td>
                                <td >
                                    <asp:Label runat="server" Width="100px" ID="Label2" Font-Bold="true" Text="" ></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="ChkNorep" runat="server"  />
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="fd" Text="Upload Report" ></asp:Label>
                                </td>
                                   <td>

                                </td>
                                <td>

                                    <asp:FileUpload ID="FUuploaReport"   runat="server"></asp:FileUpload>
                                </td>

                            </tr>

                        </table>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

                    </div>
                </div>
                 <div class="modal-footer">
          <!--<button type="button" class="btn btn-primary btn-lg" data-dismiss="modal">ADD</button>-->
                      <asp:Button ID="btnquickaddT"  class="btn btn-primary btn-lg"  runat="server" Text="Upload" ></asp:Button>

        </div>

            </div>
        </div>
    </div>
                </section>
                <!-- /.content -->
        
     <script language="javascript" type="text/javascript">
         function ValidateDelete() {
             var Check = confirm('Are you sure you want to Rerun this sample ?')
             if (Check == true) {
                 return true;
             }
             else {
                 return false;
             }
         }

 </script> 
     
</asp:Content>

