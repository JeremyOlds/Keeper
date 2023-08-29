import { RepoItem } from "./RepoItem.js";

export class Vault extends RepoItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.creator = data.creator
    this.isPrivate = data.isPrivate
  }
}