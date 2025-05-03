using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streamline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessDefinitionId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    BusinessKey = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FlowNodeId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    FlowNodeName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AssigneeId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CandidateUserIds = table.Column<string>(type: "TEXT", nullable: true),
                    CandidateGroupIds = table.Column<string>(type: "TEXT", nullable: true),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FollowUpDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: true),
                    CalledProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ErrorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    ErrorDetails = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityInstances_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Executions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentExecutionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CurrentFlowNodeId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsScope = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Executions_Executions_ParentExecutionId",
                        column: x => x.ParentExecutionId,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Executions_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EventName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ExecutionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExecutionId1 = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Configuration = table.Column<string>(type: "TEXT", nullable: true),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSubscriptions_Executions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSubscriptions_Executions_ExecutionId1",
                        column: x => x.ExecutionId1,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSubscriptions_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExecutionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExecutionId1 = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessDefinitionId = table.Column<string>(type: "TEXT", nullable: false),
                    JobHandlerType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    JobHandlerConfiguration = table.Column<string>(type: "TEXT", nullable: true),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Retries = table.Column<int>(type: "INTEGER", nullable: false),
                    LastFailureTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastFailureMessage = table.Column<string>(type: "TEXT", nullable: true),
                    LastFailureDetails = table.Column<string>(type: "TEXT", nullable: true),
                    LockOwner = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    LockExpirationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Executions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Executions_ExecutionId1",
                        column: x => x.ExecutionId1,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    ExecutionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variables_Executions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variables_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IncidentType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    IncidentTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExecutionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExecutionId1 = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProcessInstanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    JobId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProcessDefinitionId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    TenantId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_Executions_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Executions_ExecutionId1",
                        column: x => x.ExecutionId1,
                        principalTable: "Executions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidents_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_AssigneeId",
                table: "ActivityInstances",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_CalledProcessInstanceId",
                table: "ActivityInstances",
                column: "CalledProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_EndTime",
                table: "ActivityInstances",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_FlowNodeId",
                table: "ActivityInstances",
                column: "FlowNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_ProcessInstanceId",
                table: "ActivityInstances",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_StartTime",
                table: "ActivityInstances",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityInstances_State",
                table: "ActivityInstances",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_ActivityId",
                table: "EventSubscriptions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_EventName",
                table: "EventSubscriptions",
                column: "EventName");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_EventType",
                table: "EventSubscriptions",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_EventType_EventName_ExecutionId_TenantId",
                table: "EventSubscriptions",
                columns: new[] { "EventType", "EventName", "ExecutionId", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_ExecutionId",
                table: "EventSubscriptions",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_ExecutionId1",
                table: "EventSubscriptions",
                column: "ExecutionId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_ProcessInstanceId",
                table: "EventSubscriptions",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubscriptions_TenantId",
                table: "EventSubscriptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_CurrentFlowNodeId",
                table: "Executions",
                column: "CurrentFlowNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_IsActive",
                table: "Executions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_ParentExecutionId",
                table: "Executions",
                column: "ParentExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Executions_ProcessInstanceId",
                table: "Executions",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ActivityId",
                table: "Incidents",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ExecutionId",
                table: "Incidents",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ExecutionId1",
                table: "Incidents",
                column: "ExecutionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IncidentTimestamp",
                table: "Incidents",
                column: "IncidentTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IncidentType",
                table: "Incidents",
                column: "IncidentType");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_JobId",
                table: "Incidents",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ProcessInstanceId",
                table: "Incidents",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_TenantId",
                table: "Incidents",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DueDate",
                table: "Jobs",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ExecutionId",
                table: "Jobs",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ExecutionId1",
                table: "Jobs",
                column: "ExecutionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobHandlerType",
                table: "Jobs",
                column: "JobHandlerType");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_LockExpirationTime",
                table: "Jobs",
                column: "LockExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_LockOwner_LockExpirationTime",
                table: "Jobs",
                columns: new[] { "LockOwner", "LockExpirationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ProcessInstanceId",
                table: "Jobs",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Retries",
                table: "Jobs",
                column: "Retries");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TenantId",
                table: "Jobs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_BusinessKey",
                table: "ProcessInstances",
                column: "BusinessKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_EndTime",
                table: "ProcessInstances",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_ProcessDefinitionId",
                table: "ProcessInstances",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_StartTime",
                table: "ProcessInstances",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_State",
                table: "ProcessInstances",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_ExecutionId",
                table: "Variables",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_Name",
                table: "Variables",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_ProcessInstanceId",
                table: "Variables",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_ProcessInstanceId_ExecutionId_Name",
                table: "Variables",
                columns: new[] { "ProcessInstanceId", "ExecutionId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityInstances");

            migrationBuilder.DropTable(
                name: "EventSubscriptions");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Executions");

            migrationBuilder.DropTable(
                name: "ProcessInstances");
        }
    }
}
