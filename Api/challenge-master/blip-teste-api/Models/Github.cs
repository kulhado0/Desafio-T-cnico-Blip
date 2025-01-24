namespace blip_teste_api.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class GithubRepository
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("node_id")]
        public string? NodeId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("owner")]
        public GithubOwner? Owner { get; set; }

        [JsonPropertyName("html_url")]
        public string? HtmlUrl { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("fork")]
        public bool Fork { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime PushedAt { get; set; }

        [JsonPropertyName("git_url")]
        public string? GitUrl { get; set; }

        [JsonPropertyName("ssh_url")]
        public string? SshUrl { get; set; }

        [JsonPropertyName("clone_url")]
        public string? CloneUrl { get; set; }

        [JsonPropertyName("svn_url")]
        public string? SvnUrl { get; set; }

        [JsonPropertyName("homepage")]
        public string? Homepage { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonPropertyName("watchers_count")]
        public int WatchersCount { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("has_issues")]
        public bool HasIssues { get; set; }

        [JsonPropertyName("has_projects")]
        public bool HasProjects { get; set; }

        [JsonPropertyName("has_downloads")]
        public bool HasDownloads { get; set; }

        [JsonPropertyName("has_wiki")]
        public bool HasWiki { get; set; }

        [JsonPropertyName("has_pages")]
        public bool HasPages { get; set; }

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }

        [JsonPropertyName("license")]
        public GithubLicense? License { get; set; }

        [JsonPropertyName("forks_count")]
        public int ForksCount { get; set; }

        [JsonPropertyName("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonPropertyName("forks")]
        public int Forks { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName("default_branch")]
        public string? DefaultBranch { get; set; }

        [JsonPropertyName("visibility")]
        public string? Visibility { get; set; }

        [JsonPropertyName("permissions")]
        public GithubPermissions? Permissions { get; set; }
    }

    public class GithubOwner
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("html_url")]
        public string? HtmlUrl { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class GithubLicense
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("spdx_id")]
        public string? SpdxId { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class GithubPermissions
    {
        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("maintain")]
        public bool Maintain { get; set; }

        [JsonPropertyName("push")]
        public bool Push { get; set; }

        [JsonPropertyName("triage")]
        public bool Triage { get; set; }

        [JsonPropertyName("pull")]
        public bool Pull { get; set; }
    }

}
