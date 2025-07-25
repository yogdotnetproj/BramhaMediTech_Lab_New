
 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Phlebotomist2wayAccept.aspx.cs" Inherits="Phlebotomist2wayAccept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager2" runat="server">
    </asp:scriptmanager>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <%--  <Triggers>
              <asp:PostBackTrigger ControlID="btnreport" />
                <asp:PostBackTrigger ControlID="btnPdf" />
           </Triggers>--%>
        <ContentTemplate>
          
            <!-- Left side column. contains the logo and sidebar -->
           
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
     <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Phlebotomist / Sample Accept </h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Phlebotomist / Sample Accept </li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <div class="input-group date" data-provide="datepicker"  data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy" BorderColor="#0099cc"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy" BorderColor="#0099cc"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                         <asp:TextBox ID="Txt_Center" TabIndex="1" runat="server" placeholder="Enter Center" BorderColor="#0099cc" class="form-control" OnTextChanged="Txt_Center_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                        <div style="display: none; overflow: scroll; width: 200px; height: 100px" id="Div2">
                                        </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1"
                                            TargetControlID="Txt_Center"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="Fillcollection" CompletionListElementID="Div2">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                      
                                        <asp:TextBox ID="txtPatientName" runat="server" placeholder="Enter Patient Name" BorderColor="#0099cc" class="form-control pull-right" TabIndex="4">
                </asp:TextBox>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-2">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txtmobileno"  placeholder="Enter Mobile Number" BorderColor="#0099cc" TabIndex="5" runat="server"  class="form-control pull-right"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="col-sm-2">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtregno" placeholder="Enter Registration number" BorderColor="#0099cc"  runat="server"  class="form-control pull-right" TabIndex="5">
                </asp:TextBox>
                                    </div>
                                </div> 
                                 <div class="col-sm-1">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtppid" placeholder="PPID number"  runat="server" BorderColor="#0099cc"  class="form-control pull-right" TabIndex="5">
                </asp:TextBox>
                                    </div>
                                </div>   
                                <div class="col-sm-2">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtbarcodeNo" placeholder="Enter Barcode No" BorderColor="#0099cc"  runat="server"  class="form-control pull-right" TabIndex="5">
                </asp:TextBox>
                                    </div>
                                </div> 
                                 <div class="col-sm-1">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtOutbarcodeNo" placeholder=" OutBarcode No" BorderColor="#0099cc" runat="server"  class="form-control pull-right" TabIndex="6">
                </asp:TextBox>
                                    </div>
                                </div>  
                                <div class="col-sm-3">
                                                  <asp:RadioButtonList ID="RblPhenoStatus" runat="server" RepeatDirection="Horizontal" CssClass="form-check">
    <asp:ListItem>All</asp:ListItem>
    <asp:ListItem>Accept</asp:ListItem>
    <asp:ListItem  Selected="True">Pending</asp:ListItem>
                                        </asp:RadioButtonList>
                                </div>
                                <div class="col-md-1">
                                                         <asp:Button ID="Button2" runat="server" OnClientClick="return validate();" class="btn btn-primary" OnClick="Button2_Click"
                    Text="Click" TabIndex="7" />
                                </div>
                                <div id="Div1" style="display:none" runat="server" >
                                    <div class="form-group">

                                    </div>
                                    </div>                  
                            </div>
                        </div>
                       
                  
                        <div class="box-body">
                            <div class="table-responsive" style=" overflow: scroll;  height: 900px; text-align: left">
                <asp:GridView ID="GV_Phlebotomist" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="PID,SrNo,PatRegID"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 OnPageIndexChanging="GV_Phlebotomist_PageIndexChanging"
                    PageSize="10" AllowPaging="true" OnRowEditing="GV_Phlebotomist_RowEditing1" OnRowDeleting="GV_Phlebotomist_RowDeleting1"
                    OnRowDataBound="GV_Phlebotomist_RowDataBound" 
                    onrowcreated="GV_Phlebotomist_RowCreated" >
<AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
<PagerSettings Visible="true"></PagerSettings>
                    <Columns>
                          <asp:BoundField DataField="DailyseqNo" HeaderText="Seq No" SortExpression="DailyseqNo" />
                        <asp:BoundField DataField="PatRegID" HeaderText="RegNo" SortExpression="PatRegID" />
                     
                         <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                           <asp:Label ID="lblPname" Width="155px" runat="server"   Text='<%#Eval("Name") %>' ></asp:Label>
                            <asp:ImageButton ID="btnEmergency" Visible="false"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="sex" HeaderText="Gender" SortExpression="sex" />
                        <asp:BoundField DataField="age" HeaderText="Age" ReadOnly="True" SortExpression="age" />
                        <asp:BoundField DataField="mdy" />
                        <asp:BoundField DataField="CenterName" HeaderText="Center " SortExpression="CenterName">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="Drname" HeaderText="Ref Doc " SortExpression="Drname">
                           
                        </asp:BoundField>                      
                       
                       
                             
                               <asp:TemplateField HeaderText="Test">
                            <ItemTemplate>
                                <asp:Label ID="txtBarcodeid11" runat="server"   Width="100%"  Text='<%#Eval("TestName") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           
                      
                        <asp:TemplateField HeaderText="BarCode">
                            <ItemTemplate>
                                <asp:TextBox ID="txtBarcodeid" runat="server" Enabled="false"  BorderStyle="Dotted" class="form-control" BorderColor="#0099cc" Width="70px"  Text='<%#Eval("BarcodeID") %>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accept/Reject">
                           
                            <ItemTemplate>
                                
                                     
                              <asp:RadioButtonList ID="RblAcceptRej" runat="server" BorderColor="#0099cc" AutoPostBack="True" 
                                    onselectedindexchanged="RblAcceptRej_SelectedIndexChanged" 
                                    RepeatDirection="Horizontal" Width="130px">
                                  <asp:ListItem Selected="True">Accept</asp:ListItem>
                                  <asp:ListItem>Reject</asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" EditText="Submit"     HeaderText="Save" ShowEditButton="True">
                            <ControlStyle Width="60px" BackColor="#18afdf"/>
                        </asp:CommandField>
                        <asp:CommandField ButtonType="Button" DeleteText=" Print" Visible="true" HeaderText="Print All"
                            ShowDeleteButton="True">
                            <ControlStyle Width="50px"  BackColor="#18afdf" />
                        </asp:CommandField>
                          
                         <asp:TemplateField HeaderText="Edit" >
                        
                            <ItemTemplate>                                
                              <asp:Button ID="btnIPrint" CommandArgument='<%#Eval("STCODE") %>' runat="server"
                                                     Text="Edit" Width="60px" OnClick="btnIPrint_Click" />
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Patient History">
                        
                            <ItemTemplate>                                
                              
                                 <asp:Label ID="lblPatientcHistory"   Text='<%#Eval("PatientcHistory") %>' ForeColor="Red"  runat="server" Width="155px"  Height="30px"
                                                    ></asp:Label> 
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Remark">
                        
                            <ItemTemplate>                                
                                 <asp:TextBox ID="txtSpecimanRemarks" placeholder=" Remark" Text='<%#Eval("SpeciamRemark") %>' Width="155px"  Height="30px" runat="server" class="form-control"
                                                    ></asp:TextBox> 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Req by Doc">
                        
                            <ItemTemplate>                                
                                 <asp:TextBox ID="txtReqbyDoc" placeholder="Req by Doc"  Text='<%#Eval("ReqbyDoc") %>' Width="155px"  Height="30px" runat="server" class="form-control"
                                                    ></asp:TextBox> 
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:BoundField DataField="SampleType" HeaderText="Sample Type" SortExpression="SampleType" >
                        
                        </asp:BoundField>
                           <asp:BoundField DataField="SpecimanNo" HeaderText="Speciman No " Visible="false" SortExpression="SpecimanNo" >
                            </asp:BoundField>
                           <asp:BoundField DataField="LabRegMediPro" HeaderText="Inv No " Visible="false" SortExpression="LabRegMediPro" >
                         
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Storage Condition" >
                        
                            <ItemTemplate>                                
                                <asp:DropDownList ID="ddloutsourceLab" runat="server" class="form-select" >
                                    <asp:ListItem Value="0" Text="select"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Room Temp(Max 6 hrs)"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Refrigerated(24 hrs)"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Refrigerated(3 days)"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Room Temp(8 hrs)"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Frozen(-80C)"></asp:ListItem>
                                   
                                </asp:DropDownList>
                                 <asp:HiddenField ID="hdnoutsourceLab" runat="server" Value='<%#Eval("OutLabName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblTestCodes" runat="server" Text='<%#Eval("STCODE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTestCharges" runat="server" Text='<%#Eval("TestCharges") %>' Visible="false"></asp:Label>
                                <asp:HiddenField ID="hdnfid" runat="server" Value='<%#Eval("FID") %>' />
                                <asp:HiddenField ID="hdnstatus" runat="server" Value='<%#Eval("IspheboAccept") %>' />
                                 <asp:HiddenField ID="hdnpid" runat="server" Value='<%#Eval("PID") %>' />
                                   <asp:Label ID="hdnsampletype" runat="server" Text='<%#Eval("SampleType") %>' Visible="false" ></asp:Label>
                                <asp:HiddenField ID="label20" runat="server" />
                                <asp:Label ID="lblTest" runat="server" Text='<%#Eval("TestName") %>' Visible="false"></asp:Label>
                                 <asp:HiddenField ID="isemergency" runat="server" Value='<%#Eval("Isemergency") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                   


 <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#38C8DD" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                   

                </asp:GridView>
                &nbsp;
                </div>
                        </div>
                    </div>
                    <div class="flashingTextcss">Text Flashing Effect</div>
                </section>
                <!-- /.content -->
           
            <!-- /.content-wrapper -->
            
        <!-- ./wrapper -->
      
        <!--  <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>-->
          <script src="plugins/Emergency.js"></script>
        <script>
            $(document).ready(function () {
                var speed = 500;
                function effectFadeIn(classname) {
                    $("." + classname).fadeOut(speed).fadeIn(speed, effectFadeOut(classname))
                }
                function effectFadeOut(classname) {
                    $("." + classname).fadeIn(speed).fadeOut(speed, effectFadeIn(classname))
                }
                //Calling fuction on pageload
                $(document).ready(function () {
                    effectFadeIn('flashingTextcss');
                });
            });
  </script>
   <script type="text/javascript">
       if (window.history.forward(1) != null)
           window.history.forward(1);
    </script>
   </ContentTemplate>
       </asp:UpdatePanel>
    
  
   </asp:Content>
