#!/bin/bash
uncrustify -c uncrustify.cfg --no-backup $(find . -name "*.cs" | grep -v svn)
