#!/usr/bin/env bash
set -euo pipefail
cd "$(realpath "$(dirname "$0")")"
if [[ -f .gitignore ]]; then
    rm -f .gitignore~
    mv .gitignore .gitignore~
fi
while read f; do
    curl -fsSL "https://github.com/github/gitignore/raw/HEAD/$f.gitignore" >> .gitignore
    printf "\n" >> .gitignore
done < gitignore_list.txt
cat gitignore_local.txt >> .gitignore
