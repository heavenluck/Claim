<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClaimProject.ReportView.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/jquery-ui-1.11.4.custom.css" rel="stylesheet" />
    <script src="/Scripts/bootbox.js"></script>
    <script src="/Scripts/HRSProjectScript.js"></script>


    <div class="card" style="z-index: 0">
        <div class="card-header card-header-warning">
            <h3 class="card-title">ตารางรายงานแสดงข้อมูลทางสถิติ</h3>
        </div>

        <hr />

        <div class="card-body table-responsive">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>


                    <div runat="server">

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group bmd-form-group">
                                    <asp:RadioButton ID="rbtBudget" runat="server" Text="&nbspปีงบประมาณ" GroupName="G1" AutoPostBack="True" OnCheckedChanged="rbtBudget_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group bmd-form-group">
                                    <asp:RadioButton ID="rbtDuration" runat="server" Text="&nbspช่วงวันที่" GroupName="G1" AutoPostBack="True" OnCheckedChanged="rbtDuration_CheckedChanged" />
                                </div>
                            </div>


                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group bmd-form-group">
                                    <asp:Label ID="lbPoint" runat="server" Text="ด่านฯ " Font-Bold="true"></asp:Label>
                                    <asp:DropDownList ID="txtStation" runat="server" CssClass="form-control custom-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group bmd-form-group">
                                    <asp:Label ID="lbBudget" runat="server" Text="ปีงบประมาณ " Font-Bold="true" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="txtBudgetYear" runat="server" Visible="false" CssClass="form-control custom-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group bmd-form-group">
                                    <asp:Label ID="lbStartDate" runat="server" Text="ตั้งแต่วันที่ " Font-Bold="true" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtStartDate" runat="server" Visible="false" CssClass="form-control datepicker" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group bmd-form-group">
                                    <asp:Label ID="lbEndDate" runat="server" Text="ถึงวันที่ " Font-Bold="true" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtEndDate" runat="server" Visible="false" CssClass="form-control datepicker" />
                                </div>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group bmd-form-group">
                                <label class="bmd-label-floating"></label>
                                <asp:Button ID="btnResult" runat="server" Text="แสดงผลลัพธ์" Visible="false" Width="80%" OnClick="btnResult_Click" class="btn btn-danger" />
                            </div>
                        </div>
                    </div>


                    <br />

                    <h3>
                        <asp:Label ID="lbTable1" runat="server" Text="" Visible="false"></asp:Label>
                    </h3>

                    <div class="col">
                        <asp:GridView
                            ID="GridViewAllbyBudget"
                            runat="server"
                            OnRowDataBound="GridViewAllbyBudget_RowDataBound"
                            OnRowCreated="GridViewAllbyBudget_RowCreated"
                            AutoGenerateColumns="False"
                            Visible="False" RowStyle-CssClass="text-center"
                            HeaderStyle-CssClass="text-center">
                            <Columns>
                                <asp:TemplateField HeaderText="ด่านฯ">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ด่านฯ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="จำนวนอุบัติเหตุทั้งหมด">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <br />

                        <h3>
                            <asp:Label ID="lbTable2" runat="server" Text="" Visible="false"></asp:Label></h3>

                    </div>

                    <asp:GridView
                        Style="table-layout: fixed;" Width="80%"
                        ID="GridViewthing"
                        runat="server"
                        OnRowDataBound="GridViewthing_RowDataBound"
                        OnRowCreated="GridViewthing_RowCreated"
                        AutoGenerateColumns="true"
                        Visible="False" RowStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                    </asp:GridView>

                    <br />
                    <asp:GridView
                        Style="table-layout: fixed;" Width="80%"
                        ID="GridViewEx"
                        runat="server"
                        OnRowDataBound="GridViewEx_RowDataBound"
                        OnRowCreated="GridViewEx_RowCreated"
                        AutoGenerateColumns="true"
                        Visible="False" RowStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                    </asp:GridView>

                    <br />
                    <asp:GridView
                        Style="table-layout: fixed;" Width="80%"
                        ID="GridViewEn2"
                        runat="server"
                        OnRowDataBound="GridViewEx_RowDataBound"
                        OnRowCreated="GridViewEn2_RowCreated"
                        AutoGenerateColumns="true"
                        Visible="False" RowStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                    </asp:GridView>
                    <br />
                    <asp:GridView
                        Style="table-layout: fixed;" Width="80%"
                        ID="GridViewEx2"
                        runat="server"
                        OnRowDataBound="GridViewEx_RowDataBound"
                        OnRowCreated="GridViewEx2_RowCreated"
                        AutoGenerateColumns="true"
                        Visible="False" RowStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>


    </div>


    <script src="/Scripts/jquery-ui-1.11.4.custom.js"></script>
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/ClaimProjectScript.js"></script>







</asp:Content>
