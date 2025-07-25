<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="RateTypeSetting.aspx.cs" Inherits="RateTypeSetting" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Rate Type Setting</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Rate Type Setting</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>Master Type</label>
                                        <div class="radio1">
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    <label>
                                                        
                                                           <asp:RadioButtonList id="rbtnRateList" runat="server" 
OnSelectedIndexChanged="rbtnRateList_SelectedIndexChanged" AutoPostBack="True" CssClass="form-check"
        RepeatDirection="Horizontal" Width="997px">
     
<asp:ListItem Value="1">Center Rate To  Center Rate</asp:ListItem>
<asp:ListItem Value="2">Center Rate To  Dr Rate</asp:ListItem>
   <asp:ListItem Value="3">Dr Rate To  Dr Rate</asp:ListItem>
<asp:ListItem Value="4">Dr Rate To  Center Rate</asp:ListItem>
</asp:RadioButtonList>     
                                                    </label>
                                                </div>    
                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Base Rate Type</label>
                                      
                                        <asp:DropDownList id="ddlratetype1" tabIndex="4" runat="server" CssClass="form-control form-select" OnSelectedIndexChanged="ddlratetype1_SelectedIndexChanged" AutoPostBack="True">
</asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Target Rate Type</label>
                                       <asp:DropDownList id="ddlratetype2" tabIndex=4 runat="server"  CssClass="form-control form-select" OnSelectedIndexChanged="ddlratetype2_SelectedIndexChanged" AutoPostBack="True">
</asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:DropDownList id="ddloprator" tabIndex="4" runat="server"  CssClass="form-control form-select" AutoPostBack="True"><asp:ListItem>+</asp:ListItem>
<asp:ListItem>-</asp:ListItem>
<asp:ListItem>*</asp:ListItem>
<asp:ListItem>/</asp:ListItem>
<asp:ListItem>%</asp:ListItem>
<asp:ListItem>=</asp:ListItem>
</asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                       
                                         <asp:TextBox id="txt_value" runat="server"  class="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                                                <div class="col-lg-12">
                                    <div class="form-group">
                                       
                                        <div class="radio">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <label>
                                                        
                                                           <asp:CheckBox id="CheckBox1" runat="server" Visible="False" CssClass="form-check"></asp:CheckBox>
                                                               <asp:Label id="lblerror" runat="server"  Visible="False" Text=" Rate is Already Exist. Are You Sure to Delete  Rate?"></asp:Label>
                                                    </label>
                                                </div>    
                                                <div class="col-lg-3 col-xs-3">
                                                    <label>
                                                      
                                                    </label>
                                                </div>
                                                <div class="col-lg-3 col-xs-3">
                                                    <label>
                                                              
                                                    </label>
                                                </div>
                                                <div class="col-lg-3 col-xs-3">
                                                    <label>
                                                               
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                               
                                       <asp:Button id="btnSubmit" onclick="btnSubmit_Click" runat="server" class="btn btn-success" Visible="False" Text="Submit" OnClientClick="return selectdd();"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            
</asp:Content>

