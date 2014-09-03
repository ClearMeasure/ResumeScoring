Resume Scoring
============

The resume scoring app will evolve into two projects.

1. A new applicant resume scoring component to aide in silencing the noise involved in screening new applicants
2. A C# api wrapper for Resumator to be published in NUGET and open sourced for all to use

## 1 - New applicant resume scoring component

The first most important feature for Clear Measure is the ability to have a configurable way to score resumes based on weighting various keywords in a resume.  Some words might be required.  Some words may boost the score of a resume.  Some words may decrease a resumes score.  Resumes that score over a set thresh-hold are left in the queue of new hires.  Resumes that don't fare so well might be toggled to a review status.  Resumes that utterly fail might be automatically flagged as failing the screening.

The scoring component should call the API a couple of times a day and iterate through all "new" status candidates.  For candidates with resumes, the resume should be parsed and the candidate status should be toggled to the next most appropriate status (not left in new).  A comment should be added to the candidate with the resume scoring results.

The scoring component will need to learn over time.  This will be achieved by defining a collection of word groupings with rules pertaining to each group.  

- Good "if this word appears, bump the score by X"
- Good "if this word appears, decrement the score by X"

### Can be a json file in a cloud share location.

### This might end up being an azure worker role.

## 2 - Open source C# Resumator API wrapper 

The outcome of this application should be a fairly useable component for interacting with the Resumator API.
 
