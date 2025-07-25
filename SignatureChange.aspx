<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master"  CodeFile="SignatureChange.aspx.cs" Inherits="SignatureChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                             <!-- Content Header (Page header) -->               
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Signature Change</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Signature Change</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                      
                                         <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                         <asp:TextBox ID="Txt_Center" TabIndex="1" runat="server" placeholder="Enter Center" class="form-control" OnTextChanged="Txt_Center_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                        <div style="display: none; overflow: scroll; width: 200px; height: 100px" id="Div2">
                                        </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1"
                                            TargetControlID="Txt_Center" ServiceMethod="Fillcollection" CompletionListElementID="Div2">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-lg-4 mt-2">
                                    <div class="form-group">
                                      
                                      
                                        <asp:TextBox ID="txtPatientName" runat="server" placeholder="Enter Patient Name" class="form-control pull-right" TabIndex="4">
                </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4  mt-2">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txtmobileno"  placeholder="Enter Mobile Number" TabIndex="5" runat="server"  class="form-control pull-right"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="col-lg-4  mt-2">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtregno" placeholder="Enter Registration(PPID) number"  runat="server"  class="form-control pull-right" TabIndex="5">
                </asp:TextBox>
                                    </div>
                                </div>  
                                <div class="col-lg-12" runat="server" visible="false" >
                                    <div class="form-group">
<asp:RadioButtonList ID="RblPhenoStatus" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem Selected="True">All</asp:ListItem>
    <asp:ListItem>Accept</asp:ListItem>
    <asp:ListItem  >Pending</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    </div>                  
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mt-2">
                                <div class="col-lg-12 text-center">
                                   
                                     <asp:Button ID="Button2" runat="server" OnClientClick="return validate();" class="btn btn-primary" OnClick="Button2_Click"
                    Text="Click" TabIndex="7" />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style=" overflow: scroll;  height: 900px; text-align: left">
                <asp:GridView ID="GV_Phlebotomist" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="PID,SrNo,PatRegID"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 OnPageIndexChanging="GV_Phlebotomist_PageIndexChanging"
                    PageSize="50" OnRowEditing="GV_Phlebotomist_RowEditing1" OnRowDeleting="GV_Phlebotomist_RowDeleting1"
                    OnRowDataBound="GV_Phlebotomist_RowDataBound" 
                    onrowcreated="GV_Phlebotomist_RowCreated" >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
<PagerSettings Visible="true"></PagerSettings>
                    <Columns>
                        <asp:BoundField DataField="DailyseqNo" HeaderText="Seq No" SortExpression="DailyseqNo" />
                        <asp:BoundField DataField="PatRegID" HeaderText="RegNo" SortExpression="PatRegID" />
                     
                         <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                           <asp:Label ID="lblPname" runat="server"   Text='<%#Eval("Name") %>' ></asp:Label>
                            

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
                                <asp:Label ID="txtBarcodeid11" runat="server" Width="100%"  Text='<%#Eval("TestName") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:BoundField DataField="FirstTechnican" HeaderText="First Technican " SortExpression="FirstTechnican">                           
                        </asp:BoundField> 
                        <asp:BoundField DataField="SecondTechnican" HeaderText="Second Technican " SortExpression="SecondTechnican">                           
                        </asp:BoundField>   
                      
                        <asp:TemplateField HeaderText="First Technican">
                        
                            <ItemTemplate>                                
                                <asp:DropDownList ID="ddlFirstTechnican" BorderColor="#0099cc" runat="server">
                                </asp:DropDownList>
                                 <%--<asp:HiddenField ID="hdFirstTechnican" runat="server" Value='<%#Eval("OutLabName") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Second Technican">
                        
                            <ItemTemplate>                                
                                <asp:DropDownList ID="ddlSecondTechnican" BorderColor="#0099cc" runat="server">
                                </asp:DropDownList>
                                <%-- <asp:HiddenField ID="hdnSecondTechnican" runat="server" Value='<%#Eval("OutLabName") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:CommandField ButtonType="Button" EditText="Submit"     HeaderText="Save" ShowEditButton="True">
                            <ControlStyle Width="60px" BackColor="#18afdf"/>
                        </asp:CommandField>
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
                   

<HeaderStyle ForeColor="Black"></HeaderStyle>
                   

                </asp:GridView>
                &nbsp;
                </div>
                        </div>
                    </div>
                    <div class="flashingTextcss">Text Flashing Effect</div>
                </section>
                <!-- /.content -->
           
        <!-- ./wrapper -->
        </asp:Content>
